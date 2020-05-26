using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FerumChecker.Web.ViewModel.Hardware
{
    public class VideoCardViewModel
    {
        public int? Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public string ShortDescription { get; set; }

        [Required]
        public int Frequency { get; set; }

        public string FrequencyDisplay { get; set; }

        [Required]
        public int MemorySize { get; set; }

        public string MemorySizeDisplay { get; set; }


        [Required]
        public int MemoryFrequency { get; set; }

        public string MemoryFrequencyDisplay { get; set; }

        [Required]
        public int MinimumPowerConsuming { get; set; }

        public string MinimumPowerConsumingDisplay { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public IFormFile Image { get; set; }

        public string ImagePath { get; set; }

        [Required]
        public int GPUId { get; set; }

        public string GPU { get; set; }

        [Required]
        public int VideoCardInterfaceId { get; set; }

        public string VideoCardInterface { get; set; }

        [Required]
        public int ManufacturerId { get; set; }
        public string Manufacturer { get; set; }

        [Required]
        public int GraphicMemoryTypeId { get; set; }
        public string GraphicMemoryType { get; set; }


    }
}
