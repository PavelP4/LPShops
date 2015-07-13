using System;


namespace LPShops.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {        
        Repository<T> Repository<T>() where T : class;
        void Save();
    }
}
