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

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        public int Frequency { get; set; }

        [Required]
        public int MemorySize { get; set; }


        [Required]
        public int MemoryFrequency { get; set; }

        [Required]
        public int MinimumPowerConsuming { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public string Image { get; set; }

        [Required]
        [ForeignKey("GPU")]
        public int GPUId { get; set; }

        public GPU GPU { get; set; }

        [Required]
        [ForeignKey("VideoCardInterface")]
        public int VideoCardInterfaceId { get; set; }

        public VideoCardInterface VideoCardInterface { get; set; }

        [Required]
        [ForeignKey("Manufacturer")]
        public int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }

        [Required]
        [ForeignKey("GraphicMemoryType")]
        public int GraphicMemoryTypeId { get; set; }
        public GraphicMemoryType GraphicMemoryType { get; set; }
    }
}
