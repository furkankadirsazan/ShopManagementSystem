using ShopManagementSystem.WebUI.Entity;
using ShopManagementSystem.WebUI.Repository.Abstract;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace ShopManagementSystem.WebUI.Repository.Concrete.EntityFramework
{
    public class EfGenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ShopManagementSystemEntities db;
        public EfGenericRepository(ShopManagementSystemEntities _db)
        {
            db = _db;
        }
        public void Add(T entity)
        {
            db.Set<T>().Add(entity);
        }
        public void Delete(T entity)
        {
            db.Set<T>().Remove(entity);
        }
        public void Edit(T entity)
        {
            db.Entry(entity).State = EntityState.Modified;
        }
        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return db.Set<T>().Where(predicate);
        }
        public T Get(int id)
        {
            return db.Set<T>().Find(id);
        }
        public IQueryable<T> GetAll()
        {
            return db.Set<T>();
        }
        public void Save()
        {
            db.SaveChanges();
        }
    }
}