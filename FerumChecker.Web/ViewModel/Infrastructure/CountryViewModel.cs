using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FerumChecker.Web.ViewModel.Specification
{
    public class CountryViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [MaxLength(100, ErrorMessage = "Некоректне поле")]
        [MinLength(1)]
        [Display(Name = "Назва")]
        public string Name { get; set; }

        [Display(Name = "Зображення")]
        public string ImagePath { get; set; }

        [Display(Name = "Зображення")]
        public IFormFile Image { get; set; }


        [Display(Name = "Назва")]
        public string FullName { get => Name + ""; }
    }
}
