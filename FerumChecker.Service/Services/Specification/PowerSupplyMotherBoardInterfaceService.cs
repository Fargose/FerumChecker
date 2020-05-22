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
    public class PowerSupplyMotherBoardInterfaceService : IPowerSupplyMotherBoardInterfaceService
    {

        IUnitOfWork Database { get; set; }

        public PowerSupplyMotherBoardInterfaceService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public PowerSupplyMotherBoardInterface GetPowerSupplyMotherBoardInterface(int? id)
        {
            return Database.PowerSupplyMotherBoardInterfaces.Get(id.Value);
        }

        public IEnumerable<PowerSupplyMotherBoardInterface> GetPowerSupplyMotherBoardInterfaces()
        {
            return Database.PowerSupplyMotherBoardInterfaces.GetAll();
        }

        public OperationDetails UpdatePowerSupplyMotherBoardInterface(PowerSupplyMotherBoardInterface powerSupplyMotherBoardInterface)
        {
            
            Database.PowerSupplyMotherBoardInterfaces.Update(powerSupplyMotherBoardInterface);
            Database.Save();
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails CreatePowerSupplyMotherBoardInterface(PowerSupplyMotherBoardInterface powerSupplyMotherBoardInterface)
        {
            Database.PowerSupplyMotherBoardInterfaces.Create(powerSupplyMotherBoardInterface);
            Database.Save();
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails DeletePowerSupplyMotherBoardInterface(int? id)
        {
            Database.PowerSupplyMotherBoardInterfaces.Delete(id.Value);
            Database.Save();
            return new OperationDetails(true, "Ok", "");
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
