using System;


namespace Blog.Infrastructure
{
    public class PersistanceStoreException : Exception
    {
        public PersistanceStoreException(Exception exception, string message):base(message,exception)
        {
            
        }
    }
}