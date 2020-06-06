using BlogApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogApp.Repository.Abstract
{
    public interface IBlogRepository
    {
        public IQueryable<Blog> Blogs { get;}
    }
}
