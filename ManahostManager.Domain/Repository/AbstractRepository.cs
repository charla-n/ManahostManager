using ManahostManager.Domain.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ManahostManager.Domain.Repository
{
    public interface IAbstractRepository
    {
        ManahostManagerDAL RetrieveContext();
    }

    public interface IAbstractRepository<T> : IAbstractRepository
    {
        T Add(T c);

        U Add<U>(U c) where U : class;

        void Update(T c);

        void Update<U>(U obj) where U : class;

        void UpdateFields(T a, T b);

        void Delete(T c);

        IEnumerable<T> GetList(Expression<Func<T, bool>> filter = null);

        T GetUniq(Expression<Func<T, bool>> filter = null);

        void DeleteRange<U>(IEnumerable<U> obj) where U : class;

        void Delete<U>(U obj) where U : class;

        IEnumerable<U> GetList<U>(Expression<Func<U, bool>> filter = null) where U : class;

        U GetUniq<U>(Expression<Func<U, bool>> filter = null) where U : class;

        bool Save();

        Task SaveAsync();

        List<string> includes { get; set; }

        bool IgnoreIncludes { get; set; }
    }

    public abstract class AbstractRepository<T> : IAbstractRepository<T> where T : class
    {
        private bool ignoreHome;

        private ManahostManagerDAL _context;

        public List<string> includes { get; set; }

        public bool IgnoreIncludes { get; set; }

        public void InitIncludes()
        {
            if (includes.Count > 0)
                includes.Clear();
            if (!ignoreHome)
                includes.Add("Home");
            IgnoreIncludes = false;
        }

        public AbstractRepository(ManahostManagerDAL ctx, bool ignoreHome = false)
        {
            this.ignoreHome = ignoreHome;
            includes = new List<string>();
            InitIncludes();
            if (ctx == null)
            {
                _context = new ManahostManagerDAL();
            }
            else
            {
                _context = ctx;
            }
        }

        public T Add(T obj)
        {
            return _context.Set<T>().Add(obj);
        }

        public U Add<U>(U obj) where U : class
        {
            return _context.Set<U>().Add(obj);
        }

        public void Update(T obj)
        {
            _context.Entry<T>(obj).State = System.Data.Entity.EntityState.Modified;
        }

        public void Update<U>(U obj) where U : class
        {
            _context.Entry<U>(obj).State = System.Data.Entity.EntityState.Modified;
        }

        public void UpdateFields(T attached, T updated)
        {
            _context.Entry<T>(attached).CurrentValues.SetValues(updated);
        }

        public virtual void Delete(T obj)
        {
            _context.Set<T>().Remove(obj);
        }

        public void Delete<U>(U obj) where U : class
        {
            _context.Set<U>().Remove(obj);
        }

        public void DeleteRange<U>(IEnumerable<U> obj) where U : class
        {
            _context.Set<U>().RemoveRange(obj);
        }

        private IQueryable<Z> IncludeProperties<Z>(IQueryable<Z> query) where Z : class
        {
            if (!IgnoreIncludes)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }
            InitIncludes();
            return query;
        }

        public IEnumerable<T> GetList(
            Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = _context.Set<T>();

            query = IncludeProperties<T>(query);
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return query.ToList();
        }

        public IEnumerable<U> GetList<U>(
            Expression<Func<U, bool>> filter = null) where U : class
        {
            IQueryable<U> query = _context.Set<U>();

            query = IncludeProperties<U>(query);
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.ToList();
        }

        public T GetUniq(
            Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = _context.Set<T>();

            query = IncludeProperties<T>(query);
            return query.FirstOrDefault(filter);
        }

        public Task<T> GetUniqAsync(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = _context.Set<T>();

            query = IncludeProperties<T>(query);
            return query.FirstOrDefaultAsync(filter);
        }

        public U GetUniq<U>(
            Expression<Func<U, bool>> filter = null) where U : class
        {
            IQueryable<U> query = _context.Set<U>();

            query = IncludeProperties<U>(query);
            return query.FirstOrDefault(filter);
        }

        public bool Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public ManahostManagerDAL RetrieveContext()
        {
            return _context;
        }
    }
}