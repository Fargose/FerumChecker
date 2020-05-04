using FerumChecker.DataAccess.Entities.Hardware;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FerumChecker.DataAccess.Entities.Specification
{
    public class PowerSupplyCPUInterface
    {
        public PowerSupplyCPUInterface()
        {
            this.PowerSupplies = new List<PowerSupply>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public ICollection<PowerSupply> PowerSupplies = new List<PowerSupply>();
    }
}
