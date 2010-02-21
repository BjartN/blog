using System;

namespace Blog.Core.Comments
{
    public class Comment
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string IpAddress { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
    }
}