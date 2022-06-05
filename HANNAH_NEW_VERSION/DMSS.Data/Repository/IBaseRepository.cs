using DATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DATA.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        List<T> GetAll();
        void AddEntity(T entity);
        bool Insert(T entity);
        bool Update(T entity);
        bool Insert(IEnumerable<T> entities);
        bool Update(IEnumerable<T> entities);
        IQueryable<T> Table { get; }
        void RemoveEntity(T entity);
        bool Remove(T entity);
        T GetById(object id);
        bool Remove(IEnumerable<T> entities);
        List<T> GetAll(Expression<Func<T, bool>> predicate);

        List<T> GetAllPaging(Expression<Func<T, bool>> predicate, Expression<Func<T, int>> oderby, int Take, int Skip);
    }
}
