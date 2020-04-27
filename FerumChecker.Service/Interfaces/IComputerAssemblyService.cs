using FerumChecker.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Service.Interfaces
{
    public interface IComputerAssemblyService
    {
        ComputerAssembly GetComputerAssembly(int? id);
        IEnumerable<ComputerAssembly> GetComputerAssemblies();
        void Dispose();
    }
}
