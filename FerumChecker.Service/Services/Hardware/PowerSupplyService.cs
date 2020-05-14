using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.Repository.Interfaces;
using FerumChecker.Service.Infrastructure;
using FerumChecker.Service.Interfaces.Hardware;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Service.Services.Hardware
{
    public class PowerSupplyService : IPowerSupplyService
    {

        IUnitOfWork Database { get; set; }

        public PowerSupplyService(IUnitOfWork uow)
        {
            Database = uow;
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
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails CreatePowerSupply(PowerSupply powerSupply)
        {
            Database.PowerSupplies.Create(powerSupply);
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails DeletePowerSupply(int? id)
        {
            Database.PowerSupplies.Delete(id.Value);
            return new OperationDetails(true, "Ok", "");
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
