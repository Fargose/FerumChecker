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

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string Image { get; set; }


        public string FullName { get => Name + ""; }
    }
}
