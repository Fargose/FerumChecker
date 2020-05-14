using FerumChecker.DataAccess.Entities.Infrastructure;
using FerumChecker.DataAccess.Entities.Joins;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FerumChecker.DataAccess.Entities.Infrastructure
{
    public class Software
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public int MinimiumRequiredRAM { get; set; }

        public int RecommendedRequiredRAM { get; set; }

        public int DiscVolume { get; set; }
        public ICollection<SoftwareCPURequirement> SoftwareCPURequirements { get; } = new List<SoftwareCPURequirement>();

        public ICollection<SoftwareVideoCardRequirement> SoftwareVideoCardRequirements { get; } = new List<SoftwareVideoCardRequirement>();

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Required]
        [ForeignKey("Publisher")]
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }


        [Required]
        [ForeignKey("Developer")]
        public int DeveloperId { get; set; }
        public Developer Developer { get; set; }


    }
}
