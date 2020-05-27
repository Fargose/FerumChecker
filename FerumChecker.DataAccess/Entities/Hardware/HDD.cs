using FerumChecker.DataAccess.Entities.Infrastructure;
using FerumChecker.DataAccess.Entities.Specification;
using FerumChecker.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FerumChecker.DataAccess.Entities.Hardware
{
    public class HDD : IOuterMemory
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [MaxLength(200, ErrorMessage = "Некоректне поле")]
        public string Name { get; set; }

        [MaxLength(1000, ErrorMessage = "Некоректне поле")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        public int MemorySize { get; set; }

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
        [ForeignKey("OuterMemoryInterface")]
        public int OuterMemoryInterfaceId { get; set; }
        public OuterMemoryInterface OuterMemoryInterface { get; set; }


        [Required(ErrorMessage = "Поле обов'язкове")]
        [ForeignKey("OuterMemoryFormFactor")]
        public int OuterMemoryFormFactorId { get; set; }
        public OuterMemoryFormFactor OuterMemoryFormFactor { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        public int DataTransferSpeed { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        public int BufferSize { get; set; }

        public int? Year { get; set; }

    }
}
