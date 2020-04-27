using FerumChecker.DataAccess.Entities;
using FerumChecker.Repository.Interfaces;
using FerumChecker.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Service.Services
{
    public class ComputerAssemblyService : IComputerAssemblyService
    {
        IUnitOfWork Database { get; set; }

        public ComputerAssemblyService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public IEnumerable<ComputerAssembly> GetComputerAssemblies()
        {
            // применяем автомаппер для проекции одной коллекции на другую
            return Database.ComputerAssemblies.GetAll();
        }

        public ComputerAssembly GetComputerAssembly(int? id)
        {
            var computerAssembly = Database.ComputerAssemblies.Get(id.Value);

            return computerAssembly;
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
