using BlogApp.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogApp.Repository.Concrete.EntityFramework
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly BlogContext _context;

        public EfUnitOfWork(BlogContext context)
        {
            _context = context;
        }

        private IBlogRepository _blogs;
        private ICategoryRepository _categories;
        private ICommentRepository _comments;

        public IBlogRepository Blogs
        {
            get
            {
                return _blogs ?? (_blogs = new EfBlogRepository(_context));
            }
        }

        public ICategoryRepository Categories
        {
            get
            {
                return _categories ?? (_categories = new EfCategoryRepository(_context));
            }
        }

        public ICommentRepository Comments
        {
            get
            {
                return _comments ?? (_comments = new EfCommentRepository(_context));
            }
        }

        public int SaveChanges()
        {
            try
            {
              return  _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
