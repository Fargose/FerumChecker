using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.DataAccess.Entities.Specification;
using FerumChecker.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Service.Interfaces.Hardware
{
    public interface IPowerSupplyMotherBoardInterfaceService
    {
        PowerSupplyMotherBoardInterface GetPowerSupplyMotherBoardInterface(int? id);
        IEnumerable<PowerSupplyMotherBoardInterface> GetPowerSupplyMotherBoardInterfaces();

        OperationDetails CreatePowerSupplyMotherBoardInterface(PowerSupplyMotherBoardInterface powerSupplyMotherBoardInterface);

        OperationDetails UpdatePowerSupplyMotherBoardInterface(PowerSupplyMotherBoardInterface powerSupplyMotherBoardInterface);

        OperationDetails DeletePowerSupplyMotherBoardInterface(int? id);
        void Dispose();
    }
}
