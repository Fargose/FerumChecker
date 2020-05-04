using FerumChecker.DataAccess.Entities.Specification;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FerumChecker.DataAccess.Entities.Hardware
{
    public class PowerSupply
    {
        public PowerSupply()
        {
            this.PowerSupplyCPUInterfaces = new List<PowerSupplyCPUInterface>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public int Power { get; set; }

        [Required]
        public int GPUInputNumber { get; set; }


        [Required]
        public int SATAInputNumber { get; set; }

        [Required]
        public int CoolerSize { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }


        public ICollection<PowerSupplyCPUInterface> PowerSupplyCPUInterfaces { get; } = new List<PowerSupplyCPUInterface>();

        [Required]
        [ForeignKey("PowerSupplyMotherBoardInterface")]
        public int PowerSupplyMotherBoardInterfaceId { get; set; }

        public PowerSupplyMotherBoardInterface PowerSupplyMotherBoardInterface { get; set; }

        [Required]
        [ForeignKey("Manufacturer")]
        public int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }

        public ICollection<ComputerAssembly> ComputerAssemblies { get; } = new List<ComputerAssembly>();
    }
}
