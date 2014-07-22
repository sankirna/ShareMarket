using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Autofac;
using ShareMarket.BusinessLogic.Helpers;
using ShareMarket.DataAccess.Repository;
using ShareMarket.Utility.Utilities;

namespace ShareMarket.BusinessLogic.Repository
{
    public class DataRepository<T> : IDataRepository<T> where T : class
    {
        #region "Private Member(s)"

        private IComponentContext _context;
        private DbContext _dbContext;
        private DbSet<T> _dbSet;
        private bool _isDisposed;

        #endregion

        /// <summary>
        /// Public Constructor
        /// </summary>
        /// <param name="context"></param>
        public DataRepository(IComponentContext context)
        {
            try
            {
                _context = context;
                this._dbContext = context.Resolve<DbContext>();
                this._dbSet = _dbContext.Set<T>();
                this._isDisposed = false;
            }
            catch (Exception ex)
            {
                ex.LogError(this);
            }
        }

        #region "Public properties"

        /// <summary>
        /// Property fetches the total Count from the dbset.
        /// </summary>
        public int TotalCount
        {
            get { return _dbSet.Count(); }
        }

        #endregion

        #region "Public Method(s)"

        /// <summary>
        /// Method add the entity into the context.
        /// </summary>
        /// <param name="entity"></param>
        public T Add(T entity)
        {
            try
            {
                using (IWebSecurity webSecurity = _context.Resolve<IWebSecurity>())
                {
                    PropertyInfo createdOn = entity.GetType().GetProperty("CreatedOn");
                    if (createdOn != null) createdOn.SetValue(entity, DateUtil.GetCurrentDateTime(), null);

                    PropertyInfo createdByUserId = entity.GetType().GetProperty("CreatedByUserId");
                    if (createdByUserId != null) createdByUserId.SetValue(entity, webSecurity.CurrentUserId == -1 ? (int?)null : webSecurity.CurrentUserId, null); //while New User  CurrentUserId is -1. so, set it null value.

                }
                //PropertyInfo createdOn = entity.GetType().GetProperty("CreatedOn");
                //if (createdOn != null) createdOn.SetValue(entity, DateUtil.GetCurrentDateTime(), null);
                return _dbSet.Add(entity);
            }
            catch (Exception ex)
            {
                ex.LogError(this);
                return null;
            }
        }

        /// <summary>
        /// Method attaches the entity from the context
        /// </summary>
        /// <param name="entity"></param>
        public void Attach(T entity)
        {
            try
            {
                _dbSet.Attach(entity);
            }
            catch (Exception ex)
            {
                ex.LogError(this);
            }
        }

        /// <summary>
        /// Method call when explicitly updating the enteries.
        /// </summary>
        /// <param name="entity"></param>
        public void Update(T entity)
        {
            try
            {
                using (IWebSecurity webSecurity = _context.Resolve<IWebSecurity>())
                {
                    PropertyInfo modifiedOn = entity.GetType().GetProperty("UpdatedOn");
                    if (modifiedOn != null) modifiedOn.SetValue(entity, DateUtil.GetCurrentDateTime(), null);

                    //PropertyInfo modifiedBy = entity.GetType().GetProperty("ModifiedBy");
                    //if (modifiedBy != null) modifiedBy.SetValue(entity, webSecurity.CurrentUserId == -1 ? (int?)null : webSecurity.CurrentUserId, null); //while New User  CurrentUserId is -1. so, set it null value.

                }
                this.AttachEntity(entity);
            }
            catch (Exception ex)
            {
                ex.LogError(this);
            }
        }

        /// <summary>
        /// Method fetches the IQueryable based on the filter,orderby and properties to inculde.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public IQueryable<T> Fetch(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            try
            {
                IQueryable<T> query = _dbSet;

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (!String.IsNullOrWhiteSpace(includeProperties))
                {
                    query = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Aggregate(
                        query, (current, includeProperty) => current.Include(includeProperty));
                }

                return orderBy != null ? orderBy(query).AsQueryable() : query.AsQueryable();
            }
            catch (Exception ex)
            {
                ex.LogError(this);
                return null;
            }
        }

        /// <summary>
        /// Method fetches the IQueryable based on expression.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IQueryable<T> Fetch(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return _dbSet.Where(predicate).AsQueryable();
            }
            catch (Exception ex)
            {
                ex.LogError(this);
                return null;
            }
        }

        /// <summary>
        /// Method fetches the IQueryable based on filter,size and index.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="total"></param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public IQueryable<T> Fetch(Expression<Func<T, bool>> filter, out int total, int index = 0, int size = 50)
        {
            total = 0;
            try
            {
                var skipCount = index * size;
                var resetSet = filter != null ? _dbSet.Where(filter).AsQueryable() : _dbSet.AsQueryable();
                resetSet = skipCount == 0 ? resetSet.Take(size) : resetSet.Skip(skipCount).Take(size);
                total = resetSet.Count();
                return resetSet.AsQueryable();
            }
            catch (Exception ex)
            {
                ex.LogError(this);
                return null;
            }
        }

        /// <summary>
        /// Method fetches the set of record based on the supplied fucntion.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IQueryable<T> Fetch(Func<T, bool> predicate)
        {
            try
            {
                return _dbSet.Where<T>(predicate).AsQueryable();
            }
            catch (Exception ex)
            {
                ex.LogError(this);
                return null;
            }
        }

        /// <summary>
        /// Method fetches the entity based on the keys supplied.
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public T Find(params object[] keys)
        {
            try
            {
                return _dbSet.Find(keys);
            }
            catch (Exception ex)
            {
                ex.LogError(this);
                return null;
            }
        }

        /// <summary>
        /// Method fetches the entity by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetById(object id)
        {
            try
            {
                return _dbSet.Find(id);
            }
            catch (Exception ex)
            {
                ex.LogError(this);
                return null;
            }
        }

        /// <summary>
        /// Method fetches the first or default item from the datacontext based on the the supplied function.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return _dbSet.FirstOrDefault(predicate);
            }
            catch (Exception ex)
            {
                ex.LogError(this);
                return null;
            }
        }

        /// <summary>
        /// Method fetches the first or default item from the datacontext.
        /// </summary>
        /// <returns></returns>
        public T FirstOrDefault()
        {
            return _dbSet.AsQueryable().FirstOrDefault();
        }

        /// <summary>
        /// Method fetches the true/false from the datacontext based on the the supplied function.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.AsQueryable().Any(predicate);
        }

        /// <summary>
        /// Method fetches the true/false item from the datacontext.
        /// </summary>
        /// <returns></returns>
        public bool Any()
        {
            return _dbSet.AsQueryable().Any();
        }

        /// <summary>
        /// Method fetches the first record based on the supplied function.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public T First(Func<T, bool> predicate)
        {
            try
            {
                return _dbSet.First<T>(predicate);
            }
            catch (Exception ex)
            {
                ex.LogError(this);
                return null;
            }
        }

        /// <summary>
        /// Method Fetches the particular single record based on the supplied function.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public T Single(Func<T, bool> predicate)
        {
            try
            {
                return _dbSet.Single<T>(predicate);
            }
            catch (Exception ex)
            {
                ex.LogError(this);
                return null;
            }
        }

        /// <summary>
        /// Method Fetches all the data before executing query.
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAll()
        {
            try
            {
                return _dbSet.AsQueryable();
            }
            catch (Exception ex)
            {
                ex.LogError(this);
                return null;
            }
        }

        /// <summary>
        /// Method Checks whether dbset has anything entity in it or not.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public bool Contains(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return _dbSet.Any(predicate);
            }
            catch (Exception ex)
            {
                ex.LogError(this);
                return false;
            }
        }

        /// <summary>
        /// Method save the changes into the context
        /// </summary>
        public void SaveChanges()
        {
            try
            {
                _dbContext.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                string rs = "";
                foreach (var eve in e.EntityValidationErrors)
                {
                    //rs = string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                    //    eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    //Console.WriteLine(rs);

                    //foreach (var ve in eve.ValidationErrors)
                    //{
                    //    rs += "<br />" +
                    //          string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                    //}
                }
                e.LogError(this);
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                string rs = e.Message;
               
                e.LogError(this);
            }
            catch (Exception ex)
            {
                ex.LogError(this);
            }
        }

        /// <summary>
        /// Method deletes the entity from the datacontext by Id
        /// </summary>
        /// <param name="id"></param>
        public void LogicalDelete(object id)
        {
            try
            {
                var entityToDelete = _dbSet.Find(id);
                if (entityToDelete != null) LogicalDelete(entityToDelete);
            }
            catch (Exception ex)
            {
                ex.LogError(this);
            }
        }

        /// <summary>
        /// Method deletes the entity from the datacontext.
        /// </summary>
        /// <param name="entity"></param>
        public void LogicalDelete(T entity)
        {
            try
            {
                using (IWebSecurity webSecurity = _context.Resolve<IWebSecurity>())
                {
                    PropertyInfo deleted = entity.GetType().GetProperty("IsDeleted");
                    if (deleted != null) deleted.SetValue(entity, true, null);

                    PropertyInfo modifiedOn = entity.GetType().GetProperty("UpdatedOn");
                    if (modifiedOn != null) modifiedOn.SetValue(entity, DateUtil.GetCurrentDateTime(), null);

                    //PropertyInfo modifiedBy = entity.GetType().GetProperty("ModifiedBy");
                    //if (modifiedBy != null) modifiedBy.SetValue(entity, webSecurity.CurrentUserId, null);

                }
                this.AttachEntity(entity);
            }
            catch (Exception ex)
            {
                ex.LogError(this);
            }

        }

        /// <summary>
        ///   Delete objects from database by specified filter expression.
        /// </summary>
        /// <param name="predicate"></param>
        public void LogicalDelete(Expression<Func<T, bool>> predicate)
        {
            try
            {
                //var entitiesToDelete = Fetch(predicate);
                //using (IWebSecurity webSecurity = _context.Resolve<IWebSecurity>())
                //{
                //    foreach (var entity in entitiesToDelete)
                //    {
                //        PropertyInfo deleted = entity.GetType().GetProperty("IsDeleted");
                //        if (deleted != null) deleted.SetValue(entity, true, null);

                //        PropertyInfo modifiedOn = entity.GetType().GetProperty("ModifiedOn");
                //        if (modifiedOn != null) modifiedOn.SetValue(entity, DateUtil.GetCurrentDateTime(), null);

                //        PropertyInfo modifiedBy = entity.GetType().GetProperty("ModifiedBy");
                //        if (modifiedBy != null) modifiedBy.SetValue(entity, webSecurity.CurrentUserId, null);

                       

                //    }
                //}
                //this.AttachEntity(entity);
            }
            catch (Exception ex)
            {
                ex.LogError(this);
            }
        }

        /// <summary>
        /// Method deletes the entity from the datacontext by Id
        /// </summary>
        /// <param name="id"></param>
        public void Delete(object id)
        {
            try
            {
                var entityToDelete = _dbSet.Find(id);
                if (entityToDelete != null) Delete(entityToDelete);
            }
            catch (Exception ex)
            {
                ex.LogError(this);
            }
        }

        /// <summary>
        /// Method deletes the entity from the datacontext.
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(T entity)
        {
            try
            {
                this.AttachEntity(entity);
                _dbSet.Remove(entity);
            }
            catch (Exception ex)
            {
                ex.LogError(this);
            }

        }

        /// <summary>
        /// Method deletes the entity based on the supplied function.
        /// </summary>
        /// <param name="predicate"></param>
        public void Delete(Expression<Func<T, bool>> predicate)
        {
            try
            {
                var entitiesToDelete = Fetch(predicate);
                foreach (var entity in entitiesToDelete)
                {
                    this.AttachEntity(entity);
                    _dbSet.Remove(entity);
                }
            }
            catch (Exception ex)
            {
                ex.LogError(this);
            }
        }

        /// <summary>
        /// Method call on dispose calls.
        /// </summary>
        public void Dispose()
        {
            try
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            catch (Exception ex)
            {
                ex.LogError(this);
            }
        }

        /// <summary>
        /// Method Disposes the Context.
        /// </summary>
        /// <param name="disposing"></param>
        private void Dispose(bool disposing)
        {
            try
            {
                if (!_isDisposed)
                {
                    if (disposing)
                    {
                        _isDisposed = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.LogError(this);
            }

        }

        private void AttachEntity(T entity)
        {
            DbEntityEntry<T> entry = _dbContext.Entry<T>(entity);
            var idPropertyInfo = entity.GetType().GetProperties().FirstOrDefault(x => x.Name.EndsWith("Id"));

            if (idPropertyInfo != null)
            {
                int id = Convert.ToInt32(idPropertyInfo.GetValue(entity, null));

                if (entry.State == EntityState.Detached)
                {
                    var set = _dbContext.Set<T>();
                    T attachedEntity = set.Find(id); // You need to have access to key

                    if (attachedEntity != null)
                    {
                        var attachedEntry = _dbContext.Entry(attachedEntity);
                        attachedEntry.CurrentValues.SetValues(entity);
                    }
                    else
                    {
                        entry.State = EntityState.Modified; // This should attach entity
                    }
                }
            }
        }

        /// <summary>
        /// Public Destructor.
        /// </summary>
        ~DataRepository()
        {
            try
            {
                Dispose(false);
            }
            catch (Exception ex)
            {
                ex.LogError(this);
            }
        }

        #endregion

    }
}
