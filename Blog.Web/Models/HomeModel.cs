using System.Collections.Generic;
using Blog.Core;

namespace Blog.Models
{
    public class HomeModel
    {
        public IEnumerable<Post> Posts { get; set; }
        public BlogSettings Settings { get; set; }
    }
}