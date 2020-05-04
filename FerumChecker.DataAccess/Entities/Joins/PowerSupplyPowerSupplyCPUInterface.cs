using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.DataAccess.Entities.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.DataAccess.Entities.Joins
{
    public class PowerSupplyPowerSupplyCPUInterface
    {
        public int PowerSupplyId { get; set; }
        public PowerSupply PowerSupply { get; set; }

        public int PowerSupplyCPUInterfaceId { get; set; }
        public PowerSupplyCPUInterface PowerSupplyCPUInterface { get; set; }
    }
}
