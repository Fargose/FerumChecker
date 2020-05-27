using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FerumChecker.Web.ViewModel.Infrastructure
{
    public class DeveloperViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [MaxLength(100, ErrorMessage = "Некоректне поле")]
        [MinLength(1)]
        [Display(Name = "Назва")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [MaxLength(1000, ErrorMessage = "Некоректне поле")]
        [MinLength(1)]
        [Display(Name = "Опис")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        public int CountryId { get; set; }

        [Display(Name = "Назва")]
        public string FullName { get => Name; }
    }
}
