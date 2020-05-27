using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FerumChecker.Web.ViewModel.Hardware
{
    public class VideoCardViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [MaxLength(100, ErrorMessage = "Некоректне поле")]
        [Display(Name = "Назва")]
        public string Name { get; set; }

        [MaxLength(1000, ErrorMessage = "Некоректне поле")]
        [Display(Name = "Опис")]
        public string Description { get; set; }

        public string ShortDescription { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [Display(Name = "Частота ядра")]
        public int Frequency { get; set; }

        [Display(Name = "Частота ядра")]
        public string FrequencyDisplay { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [Display(Name = "Обсяг пам'яті")]
        public int MemorySize { get; set; }
        [Display(Name = "Обсяг пам'яті")]
        public string MemorySizeDisplay { get; set; }


        [Required(ErrorMessage = "Поле обов'язкове")]
        [Display(Name = "Частота пам'яті")]
        public int MemoryFrequency { get; set; }

        [Display(Name = "Частота пам'яті")]
        public string MemoryFrequencyDisplay { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [Display(Name = "Мінімальна потужність живлення")]
        public int MinimumPowerConsuming { get; set; }

        public string MinimumPowerConsumingDisplay { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [DataType(DataType.Currency)]
        [Display(Name = "Ціна")]
        public decimal Price { get; set; }
        [Display(Name = "Зображення")]
        public IFormFile Image { get; set; }

        public string ImagePath { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [Display(Name = "Графічний процесор")]
        public int GPUId { get; set; }
        [Display(Name = "Графічний процесор")]
        public string GPU { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [Display(Name = "Інтерфейс входу")]
        public int VideoCardInterfaceId { get; set; }
        [Display(Name = "Інтерфейс входу")]
        public string VideoCardInterface { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [Display(Name = "Виробник")]

        public int ManufacturerId { get; set; }
        [Display(Name = "Виробник")]
        public string Manufacturer { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [Display(Name = "Тип графічної пам'яті")]
        public int GraphicMemoryTypeId { get; set; }
        [Display(Name = "Тип графічної пам'яті")]
        public string GraphicMemoryType { get; set; }


    }
}
