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
    public class MotherBoard
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [MaxLength(200, ErrorMessage = "Некоректне поле")]
        public string Name { get; set; }

        [MaxLength(1000, ErrorMessage = "Некоректне поле")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]

        public int MaxMemory { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        public string Image { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [ForeignKey("Manufacturer")]
        public int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }


        [Required(ErrorMessage = "Поле обов'язкове")]
        [ForeignKey("MotherBoardFormFactor")]
        public int MotherBoardFormFactorId { get; set; }
        public MotherBoardFormFactor MotherBoardFormFactor { get; set; }



        [Required(ErrorMessage = "Поле обов'язкове")]
        [ForeignKey("MotherBoardNothernBridge")]
        public int MotherBoardNothernBridgeId { get; set; }
        public MotherBoardNorthBridge MotherBoardNothernBridge { get; set; }



        [Required(ErrorMessage = "Поле обов'язкове")]
        [ForeignKey("CPUSocket")]
        public int CPUSocketId { get; set; }
        public CPUSocket CPUSocket { get; set; }

        public ICollection<MotherBoardRAMSlot> MotherBoardRAMSlots { get; set; } = new List<MotherBoardRAMSlot>();

        public ICollection<MotherBoardPowerSupplySlot> PowerSupplyMotherBoardSlots { get; set; } = new List<MotherBoardPowerSupplySlot>();

        public ICollection<MotherBoardOuterMemorySlot> MotherBoardOuterMemorySlots { get; set; } = new List<MotherBoardOuterMemorySlot>();

        public ICollection<MotherBoardVideoCardSlot> MotherBoardVideoCardSlots { get; set; } = new List<MotherBoardVideoCardSlot>();

        public ICollection<ComputerAssembly> ComputerAssemblies { get; set; } = new List<ComputerAssembly>();

        public int? Year { get; set; }
    }
}
