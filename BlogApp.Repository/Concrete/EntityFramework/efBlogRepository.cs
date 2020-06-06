using BlogApp.Entity;
using BlogApp.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogApp.Repository.Concrete.EntityFramework
{
    public class efBlogRepository : IBlogRepository
    {
        private BlogContext context;

        public efBlogRepository(BlogContext _context)
        {
            context = _context;
        }

        public IQueryable<Blog> Blogs => context.Blogs;
    }
}
