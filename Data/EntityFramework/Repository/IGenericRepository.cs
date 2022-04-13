using System;
using System.Linq;
using System.Linq.Expressions;

namespace Data.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll(Expression<Func<T, bool>> where = null);
        T GetById(object id);
        void InsertOrUpdate(T obj);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object id);
        void Save();
    }
}
