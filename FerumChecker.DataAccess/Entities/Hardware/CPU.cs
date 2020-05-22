using FerumChecker.DataAccess.Entities.Infrastructure;
using FerumChecker.DataAccess.Entities.Specification;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FerumChecker.DataAccess.Entities.Hardware
{
    public class CPU
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        public int CoresNumber { get; set; }

        [Required]
        [MaxLength(100)]
        public string CoresName { get; set; }

        [Required]
        public int Frequency { get; set; }

        [Required]
        public int MaxFrequency { get; set; }

        [Required]
        public int ThreadsNumber { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public string  Image { get; set; }

        [Required]
        [ForeignKey("CPUSocket")]
        public int CPUSocketId { get; set; }
        public CPUSocket CPUSocket { get; set; }

        [Required]
        [ForeignKey("Manufacturer")]
        public int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }

        public ICollection<ComputerAssembly> ComputerAssemblies { get; } = new List<ComputerAssembly>();
    }
}
