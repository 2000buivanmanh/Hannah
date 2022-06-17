using DATA.DB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DATA.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly DMSSDbContext _context;
        private IDbSet<T> _entities;

        public BaseRepository(DMSSDbContext context)
        {
            this._context = context;
        }

        protected virtual IDbSet<T> Entities
        {
            get { return _entities ?? (_entities = _context.Set<T>()); }
        }

        protected int SaveChanges()
        {
            try
            {
                return this._context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception("save error", dbEx);
            }
        }

        public virtual List<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return this.Entities.Where(predicate).ToList();
        }

        public virtual List<T> GetAll()
        {
            return this.Entities.ToList();
        }
        public virtual void AddEntity(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            this.Entities.Add(entity);
        }
        public virtual bool Insert(T entity)
        {
            bool result = false;
            try
            {
                AddEntity(entity);

                result = this.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                // TODO: log exception
                throw ex;
            }
            return result;
        }
        public virtual bool Update(T entity)
        {
            bool result = false;
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                result = this.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                // TODO: log exception
                throw ex;
            }
            return result;
        }
        public virtual void RemoveEntity(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            this.Entities.Remove(entity);
        }

        public virtual bool Remove(T entity)
        {
            var result = false;
            try
            {
                RemoveEntity(entity);
                result = this.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public virtual bool Remove(IEnumerable<T> entities)
        {
            var result = false;
            try
            {
                if (entities == null)
                    throw new ArgumentNullException("entities");

                foreach (var entity in entities)
                {
                    this.Entities.Remove(entity);
                }

                result = this.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public T GetById(object id)
        {
            return this.Entities.Find(id);
        }

        public virtual bool Insert(IEnumerable<T> entities)
        {
            var result = false;
            try
            {
                if (entities == null)
                    throw new ArgumentNullException("entities");
                foreach (var entity in entities)
                {
                    this.Entities.Add(entity);
                    this._context.SaveChanges();
                }

                result = this.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                // TODO: log exception
                 throw ex;
            }
            return result;
        }
        public virtual bool Update(IEnumerable<T> entities)
        {
            var result = false;
            try
            {
                if (entities == null)
                    throw new ArgumentNullException("entities");
                result = this.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public virtual List<T> GetAllPaging(Expression<Func<T, bool>> predicate, Expression<Func<T, int>> oderby, int Take, int Skip)
        {
            return this.Entities.Where(predicate).OrderBy(oderby).Skip(Skip).Take(Take).ToList();
        }

        public virtual IQueryable<T> Table => this.Entities;

    }
}
