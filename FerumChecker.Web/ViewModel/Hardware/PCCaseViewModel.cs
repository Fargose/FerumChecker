using FerumChecker.Web.ViewModel.Specification;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FerumChecker.Web.ViewModel.Hardware
{
    public class PCCaseViewModel
    {
        public int? Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public string ShortDescription { get; set; }


        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public string ImagePath { get; set; }

        public IFormFile Image { get; set; }

        [Required]
        public int ManufacturerId { get; set; }
        public string Manufacturer { get; set; }

        [Required]
        public int Weight { get; set; }

        public string WeightDisplay { get; set; }


        public ICollection<MotherBoardFormFactorViewModel> PCCaseMotherBoardFormFactors { get; set; } 

        public ICollection<OuterMemoryFormFactorViewModel> PCCaseOuterMemoryFormFactors { get; set; } 

    }
}
