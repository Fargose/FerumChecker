using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FerumChecker.DataAccess.Entities.Infrastructure
{
    public class Manufacturer
    {
        [Key]
        public int Id {get ;set ;}

        [MaxLength(100, ErrorMessage = "Некоректне поле")]
        [Required(ErrorMessage = "Поле обов'язкове")]
        public string Name { get; set; }

        [MaxLength(1000, ErrorMessage = "Некоректне поле")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [ForeignKey("Country")]
        public int CountryId { get; set; }

        public Country Country { get; set; }

    }
}
