using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FerumChecker.Web.ViewModel.Specification
{
    public class VideoCardInterfaceViewModel
    {
            public int? Id { get; set; }

            [Required(ErrorMessage = "Поле обов'язкове")]
            [MaxLength(100, ErrorMessage = "Некоректне поле")]   
            [Display(Name = "Назва")]
            public string Name { get; set; }

            [Required(ErrorMessage = "Поле обов'язкове")]
            [Display(Name = "Версія")]
            public int Version { get; set; }

            [Required(ErrorMessage = "Поле обов'язкове")]
            [Display(Name = "Множник")]
            public int Multiplier { get; set; }

            [Display(Name = "Назва")]
            public string FullName { get => Name + (Multiplier == 1 ? "" : " x" + Multiplier) + (Version == 0 ? "" : " " + Version + ".0"); }
    }
}
