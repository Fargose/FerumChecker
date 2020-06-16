using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FerumChecker.Web.ViewModel.Search
{
    public class CPUSearchModel
    {
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Мінімальна ціна")]
        public decimal? MinPrice { get; set; }
        [DataType(DataType.Currency)]
        [Display(Name = "Максимальна ціна")]
        public decimal? MaxPrice { get; set; }

        [Display(Name = "Сокет")]
        public int? CPUSocketId { get; set; }

        [Display(Name = "Мінімальна частота (MHz)")]
        public int? MinFrequency { get; set; }

        [Display(Name = "Максимальна частота (MHz)")]
        public int? MaxFrequency { get; set; }

        [Display(Name = "Мінімальна кількість ядер")]
        public int? MinCores { get; set; }

        [Display(Name = "Максимальна кількість ядер")]
        public int? MaxCores { get; set; }


        [Display(Name = "Виробник")]
        public int? ManufacturerId { get; set; }
    }
}
