using BlogApp.Entity;
using BlogApp.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogApp.Repository.Concrete.EntityFramework
{
    public class EfCommentRepository : EfGenericRepository<Comment>, ICommentRepository
    {
        public EfCommentRepository(BlogContext context):base(context)
        {

        }
    }
}
