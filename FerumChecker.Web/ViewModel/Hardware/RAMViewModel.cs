using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FerumChecker.Web.ViewModel.Hardware
{
    public class RAMViewModel
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
        [Display(Name = "Частота")]
        public int Frequency { get; set; }

        public string FrequencyDisplay { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [Display(Name = "Обсяг пам'яті")]
        public int MemorySize { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [Display(Name = "Ціна")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [Display(Name = "Тип ОЗП")]
        public int RAMTypeId { get; set; }
        [Display(Name = "Тип ОЗП")]
        public string RAMType { get; set; }

        public string ImagePath { get; set; }
        [Display(Name = "Зображення")]
        public IFormFile Image { get; set; }

        [Display(Name = "Виробник")]
        [Required(ErrorMessage = "Поле обов'язкове")]
        public int ManufacturerId { get; set; }

        [Display(Name = "Виробник")]
        public string Manufacturer { get; set; }

    }
}
