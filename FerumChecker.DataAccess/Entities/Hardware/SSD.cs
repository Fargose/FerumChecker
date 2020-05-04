using FerumChecker.DataAccess.Entities.Specification;
using FerumChecker.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FerumChecker.DataAccess.Entities.Hardware
{
    public class SSD: IOuterMemory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        public int MemorySize { get; set; }

        [Required]
        [ForeignKey("Manufacturer")]
        public int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }


        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Required]
        [ForeignKey("OuterMemoryInterface")]
        public int OuterMemoryInterfaceId { get; set; }
        public OuterMemoryInterface OuterMemoryInterface { get; set; }


        [Required]
        [ForeignKey("OuterMemoryFormFactor")]
        public int OuterMemoryFormFactorId { get; set; }
        public OuterMemoryFormFactor OuterMemoryFormFactor { get; set; }

        [Required]
        public int ReadSpeed { get; set; }

        [Required]
        public int WriteSpeed { get; set; }

        public ICollection<ComputerAssembly> ComputerAssemblies { get; } = new List<ComputerAssembly>();

    }
}
