using System;
namespace Blog.Infrastructure.MetaWeblogApi
{
    public class NoSuchPostException : Exception
    {
        public NoSuchPostException(Exception inner,string id)
            : base(string.Format("Cannot find post with id {0}",id), inner)
        {
        }
    }
}