using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FerumChecker.Web.ViewModel.Hardware
{
    public class CPUViewModel
    {
        public int? Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        public string ShortDescription { get; set; }

        [Required]
        public int CoresNumber { get; set; }

        [Required]
        [MaxLength(100)]
        public string CoresName { get; set; }

        [Required]
        public int Frequency { get; set; }

        public string FrequencyDisplay { get; set; }

        [Required]
        public int MaxFrequency { get; set; }

        public string MaxFrequencyDisplay { get; set; }

        [Required]
        public int ThreadsNumber { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public string ImagePath { get; set; }

        [Display(Name = "Зображення")]
        public IFormFile Image { get; set; }

        [Required]
        public int CPUSocketId { get; set; }
        public string CPUSocket { get; set; }

        [Required]
        public int ManufacturerId { get; set; }
        public string Manufacturer { get; set; }
    }
}
