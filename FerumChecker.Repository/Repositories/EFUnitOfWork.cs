using FerumChecker.DataAccess.Entities;
using FerumChecker.Repository.EF;
using FerumChecker.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Repository.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private ApplicationContext db;
        private ComputerAssemblyRepository computerAssemblyRepository;

        public EFUnitOfWork(ApplicationContext context)
        {
            db = context;
        }
        public IRepository<ComputerAssembly> ComputerAssemblies
        {
            get
            {
                if (computerAssemblyRepository == null)
                    computerAssemblyRepository = new ComputerAssemblyRepository(db);
                return computerAssemblyRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

