using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.Repository.Interfaces;
using FerumChecker.Service.Infrastructure;
using FerumChecker.Service.Interfaces.Hardware;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Service.Services.Hardware
{
    public class RAMService : IRAMService
    {

        IUnitOfWork Database { get; set; }

        public RAMService(IUnitOfWork uow)
        {
            Database = uow;
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
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails CreateRAM(RAM ram)
        {
            Database.RAMs.Create(ram);
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails DeleteRAM(int? id)
        {
            Database.RAMs.Delete(id.Value);
            return new OperationDetails(true, "Ok", "");
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
