using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.DataAccess.Entities.Specification;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FerumChecker.DataAccess.Entities.Joins
{
    public class PowerSupplyPowerSupplyCPUInterface
    {

        [Key]
        public int Id { get; set; }


        [Required]
        [ForeignKey("PowerSupply")]
        public int PowerSupplyId { get; set; }
        public PowerSupply PowerSupply { get; set; }


        [Required]
        [ForeignKey("PowerSupplyCPUInterface")]
        public int PowerSupplyCPUInterfaceId { get; set; }
        public PowerSupplyCPUInterface PowerSupplyCPUInterface { get; set; }
    }
}
