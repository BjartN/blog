using System.Collections.Generic;

namespace Blog.Core.Comments
{
    public class Thread
    {
        public string Url { get; set; }

        public IEnumerable<Comment> Comments { get; set; }
    }
}