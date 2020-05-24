using FerumChecker.Web.ViewModel.Specification;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FerumChecker.Web.ViewModel.Hardware
{
    public class MotherBoardViewModel
    {
        public int? Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public string ShortDescription { get; set; }

       [Required]
        public int MaxMemory { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public string ImagePath { get; set; }

        public IFormFile Image { get; set; }

        [Required]
        public int ManufacturerId { get; set; }
        public string Manufacturer { get; set; }


        [Required]
        public int MotherBoardFormFactorId { get; set; }
        public string MotherBoardFormFactor { get; set; }



        [Required]
        public int MotherBoardNothernBridgeId { get; set; }
        public string MotherBoardNothernBridge { get; set; }



        [Required]
        public int CPUSocketId { get; set; }
        public string CPUSocket { get; set; }

        public List<RAMTypeViewModel> MotherBoardRAMSlots { get; set; }

        public List<PowerSupplyMotherBoardInterfaceViewModel> PowerSupplyMotherBoardInterfaces { get; set; }

        public List<OuterMemoryInterfaceViewModel> OuterMemoryInterfaces { get; set; }

        public List<VideoCardInterfaceViewModel> VideoCardInterfaces { get; set; }

    }
}
