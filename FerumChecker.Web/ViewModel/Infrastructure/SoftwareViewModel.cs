using FerumChecker.DataAccess.Entities.Joins;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FerumChecker.Web.ViewModel.Infrastructure
{
    public class SoftwareViewModel
    {

        public int? Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public string ShortDescription { get; set; }

        public int MinimiumRequiredRAM { get; set; }

        public string MinimiumRequiredRAMDisplay { get; set; }

        public int RecommendedRequiredRAM { get; set; }

        public string RecommendedRequiredRAMDisplay { get; set; }

        public int DiscVolume { get; set; }
        public string DiscVolumeDisplay { get; set; }
        public ICollection<SoftwareCPURequirement> SoftwareCPURequirements { get; set; }
        public string RecomendedCPURequirmentsDisplay { get; set; }
        public string MinimumCPURequirmentsDisplay { get; set; }
        public ICollection<SoftwareVideoCardRequirement> SoftwareVideoCardRequirements { get; set; } = new List<SoftwareVideoCardRequirement>();
        public string RecomendedVideoCardRequirmentsDisplay { get; set; }

        public string MinimumVideoCardRequirmentsDisplay { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        public int PublisherId { get; set; }
        public string Publisher { get; set; }


        [Required]
        public int DeveloperId { get; set; }
        public string Developer { get; set; }

        public IFormFile Image { get; set; }

        public string ImagePath { get; set; }
    }
}
