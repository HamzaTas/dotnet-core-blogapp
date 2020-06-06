using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using BlogApp.Entity;

namespace BlogApp.Repository.Concrete.EntityFramework
{
    public static class FakeData
    {
        public static void EnsurePopulated(IApplicationBuilder applicationBuilder)
        {
            var scope = applicationBuilder.ApplicationServices.CreateScope();

            BlogContext dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();

            dbContext.Database.Migrate();   // running pending migration

            if (!dbContext.Blogs.Any())     // adding fake data to db if there are no records at database
            {
                var categories = new[]
                {
                    new Category(){ CategoryName = ".Net Core", Description = "Most valuable articles about .net Core"},
                    new Category(){ CategoryName = "Design Patters", Description = "Most valuable articles about Desşgn Patters"},
                    new Category(){ CategoryName = "Sql Server", Description = "Most valuable articles about Sql Server"},
                };

                dbContext.Categories.AddRange(categories);

                var blogs = new[]
                {
                    new Blog ()
                    {
                        Title = "Hello, ASP .NET Core!",
                        Description = "here’s the first of a new series of posts. The topic: ASP .NET Core!",
                        Content = "With all the things I’ve been working with lately, I’ve still kept up to date on what’s new with ASP .NET Core for building Web Apps, Web APIs and even full-stack C# web applications with Blazor. With the release of ASP .NET Core 2.1, and the upcoming releases of 2.2 (late 2018) and 3.0 (2019), now is a great time to be an ASP .NET Core developer. But where should you begin? You have many options.",
                        IsApproved = true,
                        CreatedTime = DateTime.Now,
                        UpdatedTime = DateTime.Now
                    },
                    new Blog ()
                    {
                        Title = "Azure Blob Storage from ASP .NET Core File Upload",
                        Description = "This is the second of a new series of posts on ASP .NET Core",
                        Content = "In the past week, I had the opportunity to participate in a hackfest with several colleagues from across the globe, to work on real-life customer projects. I took a break from my primary project to help a colleague with a simple problem: upload a file from a web browser and save it into Azure Blob Storage within an ASP .NET Core web application!",
                        IsApproved = true,
                        CreatedTime = DateTime.Now,
                        UpdatedTime = DateTime.Now
                    },
                    new Blog ()
                    {
                        Title = "Introduction to DESIGN PATTERNS",
                        Description = "Design patterns are typical solutions to common problems in software design. Each pattern is like a blueprint that you can customize to solve a particular design problem in your code.",
                        Content = "Patterns are a toolkit of solutions to common problems in software design. They define a common language that helps your team communicate more efficiently.",
                        IsApproved = true,
                        CreatedTime = DateTime.Now,
                        UpdatedTime = DateTime.Now
                    },
                    new Blog ()
                    {
                        Title = "SQL Server Authentication",
                        Description = "Authentication, The process of identity verification of the user.",
                        Content = "Authentication, The process of identity verification of the user. In Laymen terms, Attention is the process to check “Who are you?”. It can be based on user-id & password or token-based or certificate-based. Authentication is the key to allow only authorized users can connect to access the data and system.",
                        IsApproved = true,
                        CreatedTime = DateTime.Now,
                        UpdatedTime = DateTime.Now
                    },

                };

                dbContext.Blogs.AddRange(blogs);

                var comments = new[]
                {
                    new Comment()
                    {
                        Name = "Shahed",
                        Surname = "Rose",
                        Email = "shahed.rose@gmail.com",
                        Content = "Yep, exciting times ahead with ASP .NET Core!",
                        WebSite = null,
                        Blog = blogs[0]
                    },
                    new Comment()
                    {
                        Name = "Hamilton",
                        Surname = "Stone",
                        Email = "hamilton.stone@yahoo.com",
                        Content = "You’re welcome, thanks for reading!",
                        WebSite = null,
                        Blog = blogs[0]
                    },
                     new Comment()
                    {
                        Name = "Zhang",
                        Surname = "Lingkang",
                        Email = "Lingkang.Zhang@yahoo.com",
                        Content = "The book is awesome, easy-understanding and well-written. Just have a little suggestion to organize the content not in alphabetical order but by categories would be better. And also put some code in it [rather than having it in separate archive] so that it would be easier to read on an iPad when travel.",
                        WebSite = null,
                        Blog = blogs[2]
                    },
                      new Comment()
                    {
                        Name = "Christopher",
                        Surname = "Lousberg",
                        Email = "Lousberg.Christopher@yahoo.com",
                        Content = "I read it the same day I got it, I mostly use it as a refresher on on when I dont see the woods for the trees. I think it's fine the way it is.",
                        WebSite = null,
                        Blog = blogs[2]
                    },
                };

                dbContext.Comments.AddRange(comments);

                var blogCategories = new[]
                {
                    new BlogCategory(){Blog = blogs[0] , Category = categories[0]},
                    new BlogCategory(){Blog = blogs[1] , Category = categories[0]},
                    new BlogCategory(){Blog = blogs[2] , Category = categories[1]},
                    new BlogCategory(){Blog = blogs[3] , Category = categories[2]},
                };

                dbContext.AddRange(blogCategories);
                dbContext.SaveChanges();

            }
        }

    }
}
