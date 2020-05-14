using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.Repository.Interfaces;
using FerumChecker.Service.Infrastructure;
using FerumChecker.Service.Interfaces.Hardware;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Service.Services.Hardware
{
    public class PCCaseService : IPCCaseService
    {

        IUnitOfWork Database { get; set; }

        public PCCaseService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public PCCase GetPCCase(int? id)
        {
            return Database.PCCases.Get(id.Value);
        }

        public IEnumerable<PCCase> GetPCCases()
        {
            return Database.PCCases.GetAll();
        }

        public OperationDetails UpdatePCCase(PCCase pcCase)
        {
            
            Database.PCCases.Update(pcCase);
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails CreatePCCase(PCCase pcCase)
        {
            Database.PCCases.Create(pcCase);
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails DeletePCCase(int? id)
        {
            Database.PCCases.Delete(id.Value);
            return new OperationDetails(true, "Ok", "");
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
