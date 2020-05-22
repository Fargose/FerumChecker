﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FerumChecker.Web.ViewModel.Specification
{
    public class CountryViewModel
    {
        public int? Id { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        [Display(Name = "Назва")]
        public string Name { get; set; }

        [Display(Name = "Зображення")]
        public string Image { get; set; }


        [Display(Name = "Назва")]
        public string FullName { get => Name + ""; }
    }
}
