using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.DataAccess.Entities.Joins;
using FerumChecker.DataAccess.Entities.Specification;
using FerumChecker.Repository.Interfaces;
using FerumChecker.Service.Infrastructure;
using FerumChecker.Service.Interfaces.Hardware;
using FerumChecker.Service.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FerumChecker.Service.Services.Hardware
{
    public class PowerSupplyService : IPowerSupplyService
    {

        IUnitOfWork Database { get; set; }

        IComputerAssemblyService _computerAssemblyService { get; set; }

        public PowerSupplyService(IUnitOfWork uow, IComputerAssemblyService computerAssemblyService)
        {
            Database = uow;
            _computerAssemblyService = computerAssemblyService;

        }

        public PowerSupply GetPowerSupply(int? id)
        {
            return Database.PowerSupplies.Get(id.Value);
        }

        public IEnumerable<PowerSupply> GetPowerSupplies()
        {
            return Database.PowerSupplies.GetAll();
        }

        public OperationDetails UpdatePowerSupply(PowerSupply powerSupply)
        {
            
            Database.PowerSupplies.Update(powerSupply);
            //SetPowerSupplyCPUInterfaces(powerSupply, (List<PowerSupplyPowerSupplyCPUInterface>)powerSupply.PowerSupplyPowerSupplyCPUInterfaces);
            Database.Save();

            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails CreatePowerSupply(PowerSupply powerSupply)
        {
            Database.PowerSupplies.Create(powerSupply);
            SetPowerSupplyCPUInterfaces(powerSupply, (List<PowerSupplyPowerSupplyCPUInterface>)powerSupply.PowerSupplyPowerSupplyCPUInterfaces);
            Database.Save();

            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails DeletePowerSupply(int? id)
        {
            Database.PowerSupplies.Delete(id.Value);
            Database.Save();

            return new OperationDetails(true, "Ok", "");
        }

        private OperationDetails SetPowerSupplyCPUInterfaces(PowerSupply powerSupply, List<PowerSupplyPowerSupplyCPUInterface> videoCards)
        {
            if (powerSupply.Id > -1 && videoCards != null)
            {
                var oldInterfaces = Database.PowerSupplyPowerSupplyCPUInterfaces.GetAll().Where(m => m.PowerSupplyCPUInterfaceId == powerSupply.Id);
                foreach (var item in oldInterfaces)
                {
                    Database.PowerSupplyPowerSupplyCPUInterfaces.Delete(item.Id);
                }
                foreach (var item in videoCards)
                {
                    item.PowerSupplyId = powerSupply.Id;
                    this.Database.PowerSupplyPowerSupplyCPUInterfaces.Create(item);
                }
            }

            return new OperationDetails(true, "Ok", "");
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
