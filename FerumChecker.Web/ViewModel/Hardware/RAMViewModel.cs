using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FerumChecker.Web.ViewModel.Hardware
{
    public class RAMViewModel
    {
        public int? Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public string ShortDescription { get; set; }

        [Required]
        public int Frequency { get; set; }

        public string FrequencyDisplay { get; set; }

        [Required]
        public int MemorySize { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int RAMTypeId { get; set; }
        public string RAMType { get; set; }

        public string ImagePath { get; set; }

        public IFormFile Image { get; set; }
        public int ManufacturerId { get; set; }
        public string Manufacturer { get; set; }

    }
}
