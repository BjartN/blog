using System;
using System.Collections.Generic;

namespace Blog.Infrastructure.Tumblr
{
    public class Post
    {
        public string Url { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime GmtDate { get; set; }
        public string Id { get; set; }
        public IList<string> Tags { get; set; }
    }
}