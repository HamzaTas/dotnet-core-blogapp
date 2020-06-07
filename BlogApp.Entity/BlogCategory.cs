using System;
using System.Collections.Generic;
using System.Text;

namespace BlogApp.Entity
{
    public class BlogCategory:IEntity
    {
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
