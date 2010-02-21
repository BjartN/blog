using System;


namespace Blog.Infrastructure.Db4o
{
    public class PersistanceStoreException : Exception
    {
        public PersistanceStoreException(Exception exception, string message):base(message,exception)
        {
            
        }
    }
}
