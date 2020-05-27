using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FerumChecker.DataAccess.Entities.Specification
{
    public class VideoCardInterface
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [MaxLength(100, ErrorMessage = "Некоректне поле")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        public int Version { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        public int Multiplier { get; set; }
    }
}
