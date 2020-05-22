using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FerumChecker.Web.ViewModel.Infrastructure
{
    public class ManufacturerViewModel
    {
        public int? Id { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        [Display(Name = "Назва")]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000)]
        [MinLength(1)]
        [Display(Name = "Опис")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        public int CountryId { get; set; }

        public string FullName { get => Name; }
    }
}
