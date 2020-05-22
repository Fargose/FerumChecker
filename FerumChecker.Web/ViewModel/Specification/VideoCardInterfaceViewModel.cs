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

            [Required]
            [MaxLength(100)]   
            [Display(Name = "Назва")]
            public string Name { get; set; }

            [Required]
            [Display(Name = "Версія")]
            public int Version { get; set; }

            [Required]
            [Display(Name = "Множник")]
            public int Multiplier { get; set; }

            [Display(Name = "Назва")]
            public string FullName { get => Name + (Multiplier == 1 ? "" : " x" + Multiplier) + (Version == 0 ? "" : " " + Version + ".0"); }
    }
}
