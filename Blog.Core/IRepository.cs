using System;
using System.Linq;

namespace Blog.Core
{
    public interface IRepository
    {
        IQueryable<T> List<T>();
        void Save<T>(T obj);
        void Delete<T>(T obj);
        T Get<T>(string id);
    }
}