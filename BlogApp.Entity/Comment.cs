using System;
using System.Collections.Generic;
using System.Text;

namespace BlogApp.Entity
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string WebSite { get; set; }
        public string Content { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}
