using FerumChecker.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<ComputerAssembly> ComputerAssemblies { get; }
        void Save();
    }
}
