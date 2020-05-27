using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.Repository.Interfaces;
using FerumChecker.Service.Infrastructure;
using FerumChecker.Service.Interfaces.Hardware;
using FerumChecker.Service.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Service.Services.Hardware
{
    public class RAMService : IRAMService
    {

        IUnitOfWork Database { get; set; }
        IComputerAssemblyService _computerAssemblyService { get; set; }
        public RAMService(IUnitOfWork uow, IComputerAssemblyService computerAssemblyService)
        {
            Database = uow;
            _computerAssemblyService = computerAssemblyService;
        }

        public RAM GetRAM(int? id)
        {
            return Database.RAMs.Get(id.Value);
        }

        public IEnumerable<RAM> GetRAMs()
        {
            return Database.RAMs.GetAll();
        }

        public OperationDetails UpdateRAM(RAM ram)
        {
            
            Database.RAMs.Update(ram);
            Database.Save();
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails CreateRAM(RAM ram)
        {
            Database.RAMs.Create(ram);
            Database.Save();
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails DeleteRAM(int? id)
        {
            Database.RAMs.Delete(id.Value);
            Database.Save();
            return new OperationDetails(true, "Ok", "");
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
