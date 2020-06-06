using BlogApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BlogApp.Repository.Abstract
{
    public interface IBlogRepository:IGenericRepository<Blog>
    {
        List<Blog> GetTop10Blogs();
    }
}
