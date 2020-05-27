using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FerumChecker.DataAccess.Entities.Infrastructure
{
    public class Country
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [MaxLength(100, ErrorMessage = "Некоректне поле")]
        public string Name { get; set; }

        public string Image { get; set; }

        public IEnumerable<Manufacturer> Manufacturers { get; set; }
        public IEnumerable<Publisher> Publishers { get; set; }
        public IEnumerable<Developer> Developers { get; set; }
    }
}
