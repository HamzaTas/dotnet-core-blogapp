using System;
using System.Collections.Generic;
using System.Text;

namespace BlogApp.Entity
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
        public ICollection<BlogCategory> BlogCategories { get; set; }
    }
}
