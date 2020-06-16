using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FerumChecker.Web.ViewModel.Search
{
    public class SoftwareSearchModel
    {
        public string Name { get; set; }

        [Display(Name = "Видавець")]
        public int? PublisherId { get; set; }


        [Display(Name = "Розробник")]
        public int? DeveloperId { get; set; }

    }
}
