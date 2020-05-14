using FerumChecker.DataAccess.Entities.Infrastructure;
using FerumChecker.DataAccess.Entities.Joins;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FerumChecker.DataAccess.Entities.Hardware
{
    public class PCCase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }


        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Required]
        [ForeignKey("Manufacturer")]
        public int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }

        [Required]
        public int Weight { get; set; }


        public ICollection<PCCaseMotherBoardFormFactor> PCCaseMotherBoardFormFactors { get; } = new List<PCCaseMotherBoardFormFactor>();

        public ICollection<PCCaseOuterMemoryFormFactor> PCCaseOuterMemoryFormFactors { get; } = new List<PCCaseOuterMemoryFormFactor>();

        public ICollection<ComputerAssembly> ComputerAssemblies { get; } = new List<ComputerAssembly>();

    }
}
