using FerumChecker.Web.ViewModel.Specification;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FerumChecker.Web.ViewModel.Hardware
{
    public class PowerSupplyViewModel
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
        [Display(Name = "Потужність (Вт)")]
        public int Power { get; set; }

        [Display(Name = "Потужність")]
        public string PowerDisplay { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [Display(Name = "Кількість входів для GPU")]
        public int GPUInputNumber { get; set; }


        [Required(ErrorMessage = "Поле обов'язкове")]
        [Display(Name = "Кількість входів для SATA")]
        public int SATAInputNumber { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [Display(Name = "Розмір вентилятора (мм)")]
        public int CoolerSize { get; set; }

        [Display(Name = "Розмір вентилятора")]
        public string CoolerSizeDisplay { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [DataType(DataType.Currency)]
        [Display(Name = "Ціна")]
        public decimal Price { get; set; }

        public string ImagePath { get; set; }
        [Display(Name = "Зображення")]
        public IFormFile Image { get; set; }


        [Required(ErrorMessage = "Поле обов'язкове")]
        [Display(Name = "Інтерфейс для материнської плати")]
        public int PowerSupplyMotherBoardInterfaceId { get; set; }
        [Display(Name = "Інтерфейс для материнської плати")]
        public string PowerSupplyMotherBoardInterface { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [Display(Name = "Виробник")]
        public int ManufacturerId { get; set; }
        [Display(Name = "Виробник")]
        public string Manufacturer { get; set; }

    }
}
