using System;
using System.Linq;
using Blog.Core;
using Norm;
using Norm.Linq;

namespace Blog.Infrastructure.MongoDb
{
    public class MongoRepository : IRepository, IDisposable
    {
        private readonly Mongo _db;
        private readonly MongoQueryProvider _qp;

        public MongoRepository(string uri)
        {
            _db = new Mongo(uri);
            _qp = new MongoQueryProvider(_db);
        }

        public IQueryable<T> List<T>()
        {
            try
            {
                return new MongoQuery<T>(_qp);
            }
            catch(Exception ex)
            {
                throw new PersistanceStoreException(ex, "Error accessing Db4o");
            }
        }

        public void Save<T>(T obj)
        {
            _db.GetCollection<T>().Save(obj);
        }

        public void Delete<T>(T obj)
        {
            _db.GetCollection<T>().Delete(obj);
        }

        public T Get<T>(string id)
        {
            return _db.GetCollection<T>().FindOne(new {_id=id});
        }

        public void DeleteCollection<T>()
        {
            _qp.DB.DropCollection(typeof (T).Name);
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}