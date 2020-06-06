using BlogApp.Entity;
using BlogApp.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogApp.Repository.Concrete.EntityFramework
{
    public class EfCategoryRepository : EfGenericRepository<Category>, ICategoryRepository
    {
        public EfCategoryRepository(BlogContext context):base(context)
        {

        }
    }
}
