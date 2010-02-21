using System;
using System.Linq;

namespace Blog.Core
{
    public class BlogSettings
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string VirtualMediaPath { get; set; }

        public static BlogSettings Get(IRepository repo)
        {
            return repo.List<BlogSettings>().Single();
        }
    }
}