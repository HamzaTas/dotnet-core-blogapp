using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BlogApp.Repository.Abstract;
using BlogApp.Repository.Cache;
using BlogApp.Repository.Concrete.EntityFramework;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ZNetCS.AspNetCore.Authentication.Basic;
using ZNetCS.AspNetCore.Authentication.Basic.Events;

namespace BlogApp.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //DBContext
            services.AddDbContext<BlogContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("BlogApp.WebApi")));

            //Transient for Dependency Injection
            services.AddTransient<IBlogRepository, EfBlogRepository>();
            services.AddTransient<ICategoryRepository, EfCategoryRepository>();
            services.AddTransient<ICommentRepository, EfCommentRepository>();
            services.AddTransient<IUnitOfWork, EfUnitOfWork>();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Blog API", Version = "v1" });
            });

            //Added settings
            services.AddControllers();
            services.AddControllersWithViews()
                        .AddNewtonsoftJson(options =>
                            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                        );

            //In-Memory Cache
            services.AddMemoryCache();

            //Authentication
            services.AddAuthentication(BasicAuthenticationDefaults.AuthenticationScheme).AddBasicAuthentication(
                     options =>
                     {
                         options.Realm = "Blog App";
                         options.Events = new BasicAuthenticationEvents
                         {
                             OnValidatePrincipal = context =>
                             {
                                 if ((context.UserName.ToLower() == "hamzatas") && (context.Password == "password"))
                                 {
                                     var claims = new List<Claim>
                                         {
                                            new Claim(ClaimTypes.Name,
                                                      context.UserName,
                                                      context.Options.ClaimsIssuer)
                                         };

                                     var ticket = new AuthenticationTicket(
                                        new ClaimsPrincipal(new ClaimsIdentity(
                                          claims,
                                          BasicAuthenticationDefaults.AuthenticationScheme)),
                                        new Microsoft.AspNetCore.Authentication.AuthenticationProperties(),
                                        BasicAuthenticationDefaults.AuthenticationScheme);

                                     return Task.FromResult(AuthenticateResult.Success(ticket));
                                 }

                                 return Task.FromResult(AuthenticateResult.Fail("Authentication failed."));
                             }
                         };
                     });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactor)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blog API V1");
            });

            FakeData.EnsurePopulated(app); // Control of pending migration automatically

            app.UseAuthentication(); //Authentication

            loggerFactor.AddFile("Logs/BlogApp-{Date}.txt");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
