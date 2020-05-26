using FerumChecker.Web.ViewModel.Specification;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FerumChecker.Web.ViewModel.Hardware
{
    public class PowerSupplyViewModel
    {
        public int? Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public string ShortDescription { get; set; }

        [Required]
        public int Power { get; set; }

        public string PowerDisplay { get; set; }

        [Required]
        public int GPUInputNumber { get; set; }


        [Required]
        public int SATAInputNumber { get; set; }

        [Required]
        public int CoolerSize { get; set; }

        public string CoolerSizeDisplay { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public string ImagePath { get; set; }

        public IFormFile Image { get; set; }


        public ICollection<PowerSupplyCPUInterfaceViewModel> PowerSupplyCPUInterfaces { get; set; }

        [Required]
        public int PowerSupplyMotherBoardInterfaceId { get; set; }

        public string PowerSupplyMotherBoardInterface { get; set; }

        [Required]
        public int ManufacturerId { get; set; }
        public string Manufacturer { get; set; }

    }
}
