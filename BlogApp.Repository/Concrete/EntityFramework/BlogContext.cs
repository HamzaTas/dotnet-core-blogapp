using BlogApp.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogApp.Repository.Concrete.EntityFramework
{
    public class BlogContext:DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options)
                   : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogCategory>().HasKey(pk => new { pk.BlogId, pk.CategoryId });
        }
    }
}
