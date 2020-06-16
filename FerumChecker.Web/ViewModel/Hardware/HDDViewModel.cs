using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FerumChecker.Web.ViewModel.Hardware
{
    public class HDDViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [Display(Name = "Назва")]
        public string Name { get; set; }

        [MaxLength(1000, ErrorMessage = "Некоректне поле")]
        [Display(Name = "Опис")]
        public string Description { get; set; }

        public string ShortDescription { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [Display(Name = "Обсяг пам'яті (Гб)")]
        public int MemorySize { get; set; }
        [Display(Name = "Обсяг пам'яті")]
        public string MemorySizeDisplay { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [Display(Name = "Ціна")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public IFormFile Image { get; set; }


        public string ImagePath { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [Display(Name = "Виробник")]
        public int ManufacturerId { get; set; }
        [Display(Name = "Виробник")]
        public string Manufacturer { get; set; }


        [Required(ErrorMessage = "Поле обов'язкове")]
        [Display(Name = "Інтерфейс")]
        public int OuterMemoryInterfaceId { get; set; }
        [Display(Name = "Інтерфейс")]
        public string OuterMemoryInterface { get; set; }


        [Required(ErrorMessage = "Поле обов'язкове")]
        [Display(Name = "Форм-фактор")]
        public int OuterMemoryFormFactorId { get; set; }
        [Display(Name = "Форм-фактор")]
        public string OuterMemoryFormFactor { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [Display(Name = "Швидкість передачі даних")]
        public int DataTransferSpeed { get; set; }
        [Display(Name = "Швидкість передачі даних")]
        public string DataTransferSpeedDisplay { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [Display(Name = "Розмір буфера")]
        public int BufferSize { get; set; }

        [Display(Name = "Розмір буфера")]

        public string BufferSizeDisplay { get; set; }
    }
}
