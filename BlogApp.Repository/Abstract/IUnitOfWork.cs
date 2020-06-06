using System;
using System.Collections.Generic;
using System.Text;

namespace BlogApp.Repository.Abstract
{
    public interface IUnitOfWork:IDisposable
    {
        IBlogRepository Blogs { get; }
        ICategoryRepository Categories { get; }
        ICommentRepository Comments { get; }
        int SaveChanges();
    }
}
