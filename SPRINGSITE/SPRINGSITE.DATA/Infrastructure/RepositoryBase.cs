using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Data;

namespace SPRINGSITE.DATA
{
    public abstract class RepositoryBase<T> where T : class
    {
        private AppDbContext _dataContext;

        private readonly IDbSet<T> _iDbset;

        protected RepositoryBase(IDatabaseFactory iDatabaseFactory)
        {
            DatabaseFactory = iDatabaseFactory;
            _iDbset = DataContext.Set<T>();
        }

        protected IDatabaseFactory DatabaseFactory
        {
            get;
            private set;
        }

        protected AppDbContext DataContext
        {
            get { return _dataContext ?? (_dataContext = DatabaseFactory.Get()); }
        }
        public virtual void Add(T entity)
        {
            _iDbset.Add(entity);
        }
        public virtual void Update(T entity)
        {
            _iDbset.Attach(entity);
            _dataContext.Entry(entity).State = EntityState.Modified;
        }
        public virtual void Delete(T entity)
        {
            _iDbset.Remove(entity);
        }
        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = _iDbset.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                _iDbset.Remove(obj);
        }
        public virtual T GetById(long id)
        {
            return _iDbset.Find(id);
        }
        public virtual T GetById(string id)
        {
            return _iDbset.Find(id);
        }
        public virtual IEnumerable<T> GetAll()
        {
            return _iDbset.ToList();
        }
        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return _iDbset.Where(where).ToList();
        }
        public T Get(Expression<Func<T, bool>> where)
        {
            return _iDbset.Where(where).FirstOrDefault<T>();
        }
    }
}
