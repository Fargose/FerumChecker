using FerumChecker.Web.ViewModel.Specification;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FerumChecker.Web.ViewModel.Hardware
{
    public class PCCaseViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [MaxLength(200, ErrorMessage = "Некоректне поле")]
        [Display(Name = "Назва")]
        public string Name { get; set; }

        [MaxLength(1000, ErrorMessage = "Некоректне поле")]
        [Display(Name = "Опис")]
        public string Description { get; set; }

        public string ShortDescription { get; set; }


        [Required(ErrorMessage = "Поле обов'язкове")]
        [DataType(DataType.Currency)]
        [Display(Name = "Ціна")]
        public decimal Price { get; set; }

        public string ImagePath { get; set; }
        [Display(Name = "Зображення")]
        public IFormFile Image { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [Display(Name = "Виробник")]
        public int ManufacturerId { get; set; }
        [Display(Name = "Виробник")]
        public string Manufacturer { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [Display(Name = "Вага (кг)")]
        public int Weight { get; set; }

        [Display(Name = "Вага (кг)")]
        public string WeightDisplay { get; set; }

        [Display(Name = "Форм-фактори для материнської плати")]
        public ICollection<MotherBoardFormFactorViewModel> PCCaseMotherBoardFormFactors { get; set; }

        [Display(Name = "Форм-фактори для зовнішньої пам'яті")]
        public ICollection<OuterMemoryFormFactorViewModel> PCCaseOuterMemoryFormFactors { get; set; } 

    }
}
