using System;
using System.Collections.Generic;
using System.Text;

namespace BlogApp.Entity
{
    public class Blog:IEntity
    {
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
        public bool IsApproved { get; set; }
        public ICollection<BlogCategory> BlogCategories { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
