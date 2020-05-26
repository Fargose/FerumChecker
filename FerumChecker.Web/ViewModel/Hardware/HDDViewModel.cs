using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FerumChecker.Web.ViewModel.Hardware
{
    public class HDDViewModel
    {
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public string ShortDescription { get; set; }

        [Required]
        public int MemorySize { get; set; }

        public string MemorySizeDisplay { get; set; }

        [Required]
        public decimal Price { get; set; }

        public IFormFile Image { get; set; }


        public string ImagePath { get; set; }

        [Required]
        public int ManufacturerId { get; set; }
        public string Manufacturer { get; set; }


        [Required]
        public int OuterMemoryInterfaceId { get; set; }
        public string OuterMemoryInterface { get; set; }


        [Required]
        public int OuterMemoryFormFactorId { get; set; }
        public string OuterMemoryFormFactor { get; set; }

        [Required]
        public int DataTransferSpeed { get; set; }

        public string DataTransferSpeedDisplay { get; set; }

        [Required]
        public int BufferSize { get; set; }

        public string BufferSizeDisplay { get; set; }
    }
}
