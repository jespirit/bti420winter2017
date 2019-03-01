using System;

namespace Assignment5.Data
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();
    }
}