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

        [Required]
        [MaxLength(200)]
        [Display(Name = "Назва")]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000)]
        [Display(Name = "Опис")]
        public string Description { get; set; }

        public string ShortDescription { get; set; }

        [Required]
        [Display(Name = "Кільксть ядер")]
        public int CoresNumber { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Назва ядра")]
        public string CoresName { get; set; }

        [Required]
        [Display(Name = "Частота (MHz)")]
        public int Frequency { get; set; }
        [Display(Name = "Частота")]
        public string FrequencyDisplay { get; set; }

        [Required]
        [Display(Name = "Максимальна частота (MHz)")]
        public int MaxFrequency { get; set; }
        [Display(Name = "Максимальна частота")]
        public string MaxFrequencyDisplay { get; set; }

        [Required]
        [Display(Name = "Кількість потоків")]
        public int ThreadsNumber { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Ціна")]
        public decimal Price { get; set; }
        public string ImagePath { get; set; }

        [Display(Name = "Зображення")]
        public IFormFile Image { get; set; }

        [Required]
        [Display(Name = "Сокет процесора")]
        public int CPUSocketId { get; set; }

        [Display(Name = "Сокет процесора")]
        public string CPUSocket { get; set; }

        [Required]

        [Display(Name = "Виробник")]
        public int ManufacturerId { get; set; }

        [Display(Name = "Виробник")]
        public string Manufacturer { get; set; }

        [Display(Name = "Рік випуску")]

        public int Year { get; set; }
    } 


}
