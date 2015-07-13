using System;
using System.Collections.Generic;


namespace LPShops.DataAccess
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly ShopDBContext _context;
        private bool disposed;
        private Dictionary<string, object> repositories;

        public UnitOfWork(ShopDBContext context)
        {
            this._context = context;
        }

        public UnitOfWork()
        {
            _context = new ShopDBContext();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public Repository<T> Repository<T>() where T : class
        {
            if (repositories == null)
            {
                repositories = new Dictionary<string, object>();
            }

            var type = typeof(T).Name;

            if (!repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);
                repositories.Add(type, repositoryInstance);
            }
            return (Repository<T>)repositories[type];
        }
    }
}