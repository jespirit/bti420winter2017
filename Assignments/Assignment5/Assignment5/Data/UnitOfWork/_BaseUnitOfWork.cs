using System;
using System.Data.Entity;

namespace Assignment5.Data
{
    public abstract class BaseUnitOfWork : IUnitOfWork
    {
        protected readonly DbContext _Db;

        public BaseUnitOfWork(DbContext db)
        {
            if (db == null) throw new ArgumentNullException("db");

            this._Db = db;
        }

        public int SaveChanges()
        {
            return this._Db.SaveChanges();
        } 

        public void Dispose()
        {
            if (this._Db != null)
            {
                this._Db.Dispose();
            }
        }
    }
}