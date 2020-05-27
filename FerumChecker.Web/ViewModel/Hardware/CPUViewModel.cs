using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FerumChecker.Web.ViewModel.Hardware
{
    public class CPUViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [MaxLength(200, ErrorMessage = "Некоректне поле")]
        [Display(Name = "Назва")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Поле обов'язкове")]
        [MaxLength(1000, ErrorMessage = "Некоректне поле")]
        [Display(Name = "Опис")]
        public string Description { get; set; }

        public string ShortDescription { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [Display(Name = "Кількість ядер")]
        public int CoresNumber { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [MaxLength(100, ErrorMessage = "Некоректне поле")]
        [Display(Name = "Назва ядра")]
        public string CoresName { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [Display(Name = "Частота (MHz)")]
        public int Frequency { get; set; }
        [Display(Name = "Частота")]
        public string FrequencyDisplay { get; set; }
        [Required(ErrorMessage = "Поле обов'язкове")]
        [Display(Name = "Максимальна частота (MHz)")]
        public int MaxFrequency { get; set; }
        [Display(Name = "Максимальна частота")]
        public string MaxFrequencyDisplay { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [Display(Name = "Кількість потоків")]
        public int ThreadsNumber { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [DataType(DataType.Currency)]
        [Display(Name = "Ціна")]
        public decimal Price { get; set; }
        public string ImagePath { get; set; }

        [Display(Name = "Зображення")]
        public IFormFile Image { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [Display(Name = "Сокет процесора")]
        public int CPUSocketId { get; set; }

        [Display(Name = "Сокет процесора")]
        public string CPUSocket { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]

        [Display(Name = "Виробник")]
        public int ManufacturerId { get; set; }

        [Display(Name = "Виробник")]
        public string Manufacturer { get; set; }

        [Display(Name = "Рік випуску")]

        public int Year { get; set; }
    } 


}
