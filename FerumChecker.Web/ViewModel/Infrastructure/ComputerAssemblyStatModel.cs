using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FerumChecker.Web.ViewModel.Infrastructure
{
    public class ComputerAssemblyStatModel
    {
        [Display(Name = "Частота процесора")]
        public string CPUFrequency { get; set; }

        [Display(Name = "Кількість ядер процесора")]
        public string CPUCores { get; set; }

        [Display(Name = "Кількість потоків процесора")]
        public string CPUThreads { get; set; }


        [Display(Name = "Обсяг відеопам'яті")]
        public string VideoMemory { get; set; }


        [Display(Name = "Частота ядра відеокарти")]
        public string VideoFrequency { get; set; }


        [Display(Name = "Частота відеопам'яті")]
        public string VideoMemoryFrequency { get; set; }

        [Display(Name = "Обсяг зовнішньої пам'яті")]
        public string TotalMemory { get; set; }

        [Display(Name = "Обсяг оперативної пам'яті")]
        public string TotalRam { get; set; }

        [Display(Name = "Ціна збірки")]
        [DataType(DataType.Currency)]
        public decimal TotalPrice { get; set; }
    }
}
