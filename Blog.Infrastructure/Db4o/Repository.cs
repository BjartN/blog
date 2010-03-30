using System;
using System.Linq;
using Blog.Core;
using Db4objects.Db4o;
using Db4objects.Db4o.Linq;

namespace Blog.Infrastructure.Db4o
{
    public class Repository : IRepository, IDisposable
    {
        private IEmbeddedObjectContainer _db;

        public Repository(string fileName)
        {
            _db = Db4oEmbedded.OpenFile(Db4oEmbedded.NewConfiguration(), fileName);
        }

        public IQueryable<T> List<T>()
        {
            try
            {
                return _db.AsQueryable<T>();
            }
            catch(Exception ex)
            {
                throw new PersistanceStoreException(ex, "Error accessing Db4o");
            }
        }

        public void Save<T>(T obj)
        {
            _db.Store(obj);
        }

        public void Delete<T>(T obj)
        {
            _db.Delete(obj);
        }

        public T Get<T>(string id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}