using System;
using System.Collections.Generic;
using Blog.Core;

namespace Blog.Models
{
    public class ArchiveModel
    {
        public IList<Post> Posts { get; set; }
    }
}