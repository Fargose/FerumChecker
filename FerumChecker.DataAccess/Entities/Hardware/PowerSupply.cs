using FerumChecker.DataAccess.Entities.Infrastructure;
using FerumChecker.DataAccess.Entities.Joins;
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

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

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

        public string Image { get; set; }


        public ICollection<PowerSupplyPowerSupplyCPUInterface> PowerSupplyPowerSupplyCPUInterfaces { get; set; } 

        [Required]
        [ForeignKey("PowerSupplyMotherBoardInterface")]
        public int PowerSupplyMotherBoardInterfaceId { get; set; }

        public PowerSupplyMotherBoardInterface PowerSupplyMotherBoardInterface { get; set; }

        [Required]
        [ForeignKey("Manufacturer")]
        public int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }

        public ICollection<ComputerAssembly> ComputerAssemblies { get; } = new List<ComputerAssembly>();

        public int? Year { get; set; }
    }
}
