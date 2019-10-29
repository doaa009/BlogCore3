using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogCore3.Models
{
    public class BlogPosts
    {
        public int BlogPostsId { get; set; }
        public Name Author { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public DateTime Date { get; set; }
        public string ImageUrl { get; set; }
        public string Body { get; set; }
        
       



    }

  
}
