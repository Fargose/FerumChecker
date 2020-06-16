using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FerumChecker.Web.ViewModel.Search
{
    public class PowerSupplySearchModel
    {
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Мінімальна ціна")]
        public decimal? MinPrice { get; set; }
        [DataType(DataType.Currency)]
        [Display(Name = "Максимальна ціна")]
        public decimal? MaxPrice { get; set; }

        [Display(Name = "Мінімальна потужність")]
        public int? MinPowerty { get; set; }

        [Display(Name = "Максимальна потужність")]
        public int? MaxPowerty { get; set; }

        [Display(Name = "Виробник")]
        public int? ManufacturerId { get; set; }
    }
}
