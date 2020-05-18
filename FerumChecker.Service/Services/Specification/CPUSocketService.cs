using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.DataAccess.Entities.Specification;
using FerumChecker.Repository.Interfaces;
using FerumChecker.Service.Infrastructure;
using FerumChecker.Service.Interfaces.Hardware;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Service.Services.Hardware
{
    public class CPUSocketService : ICPUSocketService
    {

        IUnitOfWork Database { get; set; }

        public CPUSocketService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public CPUSocket GetCPUSocket(int? id)
        {
            return Database.CPUSockets.Get(id.Value);
        }

        public IEnumerable<CPUSocket> GetCPUSockets()
        {
            return Database.CPUSockets.GetAll();
        }

        public OperationDetails UpdateCPUSocket(CPUSocket cpuSocket)
        {
            
            Database.CPUSockets.Update(cpuSocket);
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails CreateCPUSocket(CPUSocket cpuSocket)
        {
            Database.CPUSockets.Create(cpuSocket);
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails DeleteCPUSocket(int? id)
        {
            Database.CPUSockets.Delete(id.Value);
            return new OperationDetails(true, "Ok", "");
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
