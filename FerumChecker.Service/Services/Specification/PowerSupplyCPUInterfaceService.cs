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
    public class PowerSupplyCPUInterfaceService : IPowerSupplyCPUInterfaceService
    {

        IUnitOfWork Database { get; set; }

        public PowerSupplyCPUInterfaceService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public PowerSupplyCPUInterface GetPowerSupplyCPUInterface(int? id)
        {
            return Database.PowerSupplyCPUInterfaces.Get(id.Value);
        }

        public IEnumerable<PowerSupplyCPUInterface> GetPowerSupplyCPUInterfaces()
        {
            return Database.PowerSupplyCPUInterfaces.GetAll();
        }

        public OperationDetails UpdatePowerSupplyCPUInterface(PowerSupplyCPUInterface powerSupplyCPUInterface)
        {
            
            Database.PowerSupplyCPUInterfaces.Update(powerSupplyCPUInterface);
            Database.Save();
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails CreatePowerSupplyCPUInterface(PowerSupplyCPUInterface powerSupplyCPUInterface)
        {
            Database.PowerSupplyCPUInterfaces.Create(powerSupplyCPUInterface);
            Database.Save();
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails DeletePowerSupplyCPUInterface(int? id)
        {
            Database.PowerSupplyCPUInterfaces.Delete(id.Value);
            Database.Save();
            return new OperationDetails(true, "Ok", "");
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
