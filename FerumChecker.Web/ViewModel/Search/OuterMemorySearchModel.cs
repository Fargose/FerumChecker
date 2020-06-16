using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FerumChecker.Web.ViewModel.Search
{
    public class OuterMemorySearchModel
    {
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Мінімальна ціна")]
        public decimal? MinPrice { get; set; }
        [DataType(DataType.Currency)]
        [Display(Name = "Максимальна ціна")]
        public decimal? MaxPrice { get; set; }

        [Display(Name = "Мінімальний обсяг")]
        public int? MinVolume { get; set; }

        [Display(Name = "Максимальний обсяг")]
        public int? MaxVolume { get; set; }

        [Display(Name = "Виробник")]
        public int? ManufacturerId { get; set; }
    }
}
