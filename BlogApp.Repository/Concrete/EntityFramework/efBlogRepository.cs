using BlogApp.Entity;
using BlogApp.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BlogApp.Repository.Concrete.EntityFramework
{
    public class EfBlogRepository : EfGenericRepository<Blog>, IBlogRepository
    {
        public EfBlogRepository(BlogContext context):base(context)
        {

        }

        public BlogContext BlogContext
        {
            get { return _context as BlogContext; }
        }

        public List<Blog> GetTop10Blogs()
        {
            return BlogContext.Blogs.OrderByDescending(x => x.BlogId).Take(5).ToList();
        }
    }
}
