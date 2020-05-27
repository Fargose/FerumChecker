using FerumChecker.DataAccess.Entities.Infrastructure;
using FerumChecker.DataAccess.Entities.Specification;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FerumChecker.DataAccess.Entities.Hardware
{
    public class VideoCard
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [MaxLength(100, ErrorMessage = "Некоректне поле")]
        public string Name { get; set; }

        [MaxLength(1000, ErrorMessage = "Некоректне поле")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        public int Frequency { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        public int MemorySize { get; set; }


        [Required(ErrorMessage = "Поле обов'язкове")]
        public int MemoryFrequency { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        public int MinimumPowerConsuming { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public string Image { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [ForeignKey("GPU")]
        public int GPUId { get; set; }

        public GPU GPU { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [ForeignKey("VideoCardInterface")]
        public int VideoCardInterfaceId { get; set; }

        public VideoCardInterface VideoCardInterface { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [ForeignKey("Manufacturer")]
        public int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [ForeignKey("GraphicMemoryType")]
        public int GraphicMemoryTypeId { get; set; }
        public GraphicMemoryType GraphicMemoryType { get; set; }

        public int? Year { get; set; }
    }
}
