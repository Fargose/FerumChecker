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

        [Required(ErrorMessage = "Поле обов'язкове")]
        [MaxLength(200, ErrorMessage = "Некоректне поле")]
        public string Name { get; set; }

        [MaxLength(1000, ErrorMessage = "Некоректне поле")]
        public string Description { get; set; }

        public int MinimiumRequiredRAM { get; set; }

        public int RecommendedRequiredRAM { get; set; }

        public int DiscVolume { get; set; }
        public ICollection<SoftwareCPURequirement> SoftwareCPURequirements { get; set; } 

        public ICollection<SoftwareVideoCardRequirement> SoftwareVideoCardRequirements { get; set; }

        public string Image { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [ForeignKey("Publisher")]
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }


        [Required(ErrorMessage = "Поле обов'язкове")]
        [ForeignKey("Developer")]
        public int DeveloperId { get; set; }
        public Developer Developer { get; set; }


    }
}
