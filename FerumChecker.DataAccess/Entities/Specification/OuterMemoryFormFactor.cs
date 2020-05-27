using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FerumChecker.DataAccess.Entities.Specification
{
    public class OuterMemoryFormFactor
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [MaxLength]
        public string Name { get; set; }
    }
}
