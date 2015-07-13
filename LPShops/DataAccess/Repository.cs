using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace LPShops.DataAccess
{
    public class Repository<T> where T : class
    {
        private readonly ShopDBContext _context;
        private IDbSet<T> _entities;
        

        public Repository(ShopDBContext context)
        {
            this._context = context;
        }

        public T GetById(object id)
        {
            return Entities.Find(id);
        }

        public void Insert(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                Entities.Add(entity);                
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(ValidationErrorMessage(dbEx), dbEx);
            }
        }

        public void Update(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }        
        
                Entities.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;                
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(ValidationErrorMessage(dbEx), dbEx);
            }
        }

        public void Delete(object id)
        {            
            Delete(GetById(id));
        }

        public void Delete(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                if (_context.Entry(entity).State == EntityState.Detached)
                {
                    Entities.Attach(entity);
                }
                Entities.Remove(entity);                
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(ValidationErrorMessage(dbEx), dbEx);
            }
        }

        private string ValidationErrorMessage(DbEntityValidationException dbEx)
        {
            string errorMessage = string.Empty;

            foreach (var validationErrors in dbEx.EntityValidationErrors)
            {
                foreach (var validationError in validationErrors.ValidationErrors)
                {
                    errorMessage += Environment.NewLine + string.Format("Property: {0} Error: {1}",
                    validationError.PropertyName, validationError.ErrorMessage);
                }
            }

            return errorMessage;
        }

        public virtual IQueryable<T> Table
        {
            get
            {
                return Entities;
            }
        }

        private IDbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                {
                    _entities = _context.Set<T>();
                }
                return _entities;
            }
        }
    }
}