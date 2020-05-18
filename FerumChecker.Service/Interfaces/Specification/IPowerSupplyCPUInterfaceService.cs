using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.DataAccess.Entities.Specification;
using FerumChecker.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Service.Interfaces.Hardware
{
    public interface IPowerSupplyCPUInterfaceService
    {
        PowerSupplyCPUInterface GetPowerSupplyCPUInterface(int? id);
        IEnumerable<PowerSupplyCPUInterface> GetPowerSupplyCPUInterfaces();

        OperationDetails CreatePowerSupplyCPUInterface(PowerSupplyCPUInterface powerSupplyCPUInterface);

        OperationDetails UpdatePowerSupplyCPUInterface(PowerSupplyCPUInterface powerSupplyCPUInterface);

        OperationDetails DeletePowerSupplyCPUInterface(int? id);
        void Dispose();
    }
}
