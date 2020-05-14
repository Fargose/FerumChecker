using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Service.Interfaces.Hardware
{
    public interface IPowerSupplyService
    {
        PowerSupply GetPowerSupply(int? id);
        IEnumerable<PowerSupply> GetPowerSupplies();

        OperationDetails CreatePowerSupply(PowerSupply powerSupply);

        OperationDetails UpdatePowerSupply(PowerSupply powerSupply);

        OperationDetails DeletePowerSupply(int? id);
        void Dispose();
    }
}
