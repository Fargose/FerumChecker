using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FerumChecker.Web.ViewModel.Specification
{
    public class OuterMemoryInterfaceViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        [Display(Name = "Назва")]
        public string Name { get; set; }
    }
}
