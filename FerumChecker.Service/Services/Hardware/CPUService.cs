using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.Repository.Interfaces;
using FerumChecker.Service.Infrastructure;
using FerumChecker.Service.Interfaces.Hardware;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Service.Services.Hardware
{
    public class CPUService : ICPUService
    {

        IUnitOfWork Database { get; set; }

        public CPUService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public CPU GetCPU(int? id)
        {
            return Database.CPUs.Get(id.Value);
        }

        public IEnumerable<CPU> GetCPUs()
        {
            return Database.CPUs.GetAll();
        }

        public OperationDetails UpdateCPU(CPU cpu)
        {
            
            Database.CPUs.Update(cpu);
            Database.Save();
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails CreateCPU(CPU cpu)
        {
            Database.CPUs.Create(cpu);
            Database.Save();
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails DeleteCPU(int? id)
        {
            Database.CPUs.Delete(id.Value);
            Database.Save();
            return new OperationDetails(true, "Ok", "");
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
