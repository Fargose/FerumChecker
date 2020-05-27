using FerumChecker.DataAccess.Entities.Joins;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FerumChecker.Web.ViewModel.Infrastructure
{
    public class SoftwareViewModel
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

        [Display(Name = "Мінімальний обсяг ОЗП")]
        public int MinimiumRequiredRAM { get; set; }
        [Display(Name = "Мінімальний обсяг ОЗП")]
        public string MinimiumRequiredRAMDisplay { get; set; }

        [Display(Name = "Рекомендований обсяг ОЗП")]
        public int RecommendedRequiredRAM { get; set; }

        [Display(Name = "Рекомендований обсяг ОЗП")]
        public string RecommendedRequiredRAMDisplay { get; set; }

        [Display(Name = "Мінімальний обсяг на диску")]

        public int DiscVolume { get; set; }

        [Display(Name = "Мінімальний обсяг на диску")]
        public string DiscVolumeDisplay { get; set; }
        public ICollection<SoftwareCPURequirement> SoftwareCPURequirements { get; set; }

        [Display(Name = "Процесор")]
        public string RecomendedCPURequirmentsDisplay { get; set; }
        [Display(Name = "Процесор")]
        public string MinimumCPURequirmentsDisplay { get; set; }
        public ICollection<SoftwareVideoCardRequirement> SoftwareVideoCardRequirements { get; set; } = new List<SoftwareVideoCardRequirement>();

        [Display(Name = "Відеокарта")]
        public string RecomendedVideoCardRequirmentsDisplay { get; set; }

        [Display(Name = "Відеокарта")]
        public string MinimumVideoCardRequirmentsDisplay { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [DataType(DataType.Currency)]
        [Display(Name = "Ціна")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [Display(Name = "Видавець")]
        public int PublisherId { get; set; }
        [Display(Name = "Видавець")]
        public string Publisher { get; set; }


        [Required(ErrorMessage = "Поле обов'язкове")]
        [Display(Name = "Розробник")]
        public int DeveloperId { get; set; }
        [Display(Name = "Розробник")]
        public string Developer { get; set; }
        [Display(Name = "Зображення")]
        public IFormFile Image { get; set; }

        public string ImagePath { get; set; }
    }
}
