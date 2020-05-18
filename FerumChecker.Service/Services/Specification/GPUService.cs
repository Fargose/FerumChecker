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
    public class GPUService : IGPUService
    {

        IUnitOfWork Database { get; set; }

        public GPUService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public GPU GetGPU(int? id)
        {
            return Database.GPUs.Get(id.Value);
        }

        public IEnumerable<GPU> GetGPUs()
        {
            return Database.GPUs.GetAll();
        }

        public OperationDetails UpdateGPU(GPU gpu)
        {
            
            Database.GPUs.Update(gpu);
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails CreateGPU(GPU gpu)
        {
            Database.GPUs.Create(gpu);
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails DeleteGPU(int? id)
        {
            Database.GPUs.Delete(id.Value);
            return new OperationDetails(true, "Ok", "");
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
