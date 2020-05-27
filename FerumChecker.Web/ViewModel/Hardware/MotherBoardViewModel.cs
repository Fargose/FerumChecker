using FerumChecker.Web.ViewModel.Specification;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FerumChecker.Web.ViewModel.Hardware
{
    public class MotherBoardViewModel
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

        [Required(ErrorMessage = "Поле обов'язкове")]
        [Display(Name = "Максимальний обсяг оперативної пам'яті (Гб)")]
        public int MaxMemory { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [DataType(DataType.Currency)]
        [Display(Name = "Ціна")]
        public decimal Price { get; set; }

        [Display(Name = "Зображення")]
        public string ImagePath { get; set; }
        [Display(Name = "Зображення")]
        public IFormFile Image { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [Display(Name = "Виробник")]
        public int ManufacturerId { get; set; }
        [Display(Name = "Виробник")]
        public string Manufacturer { get; set; }


        [Required(ErrorMessage = "Поле обов'язкове")]

        [Display(Name = "Форм-фактор")]
        public int MotherBoardFormFactorId { get; set; }
        [Display(Name = "Форм-фактор")]
        public string MotherBoardFormFactor { get; set; }



        [Required(ErrorMessage = "Поле обов'язкове")]
        [Display(Name = "Чіп-сет")]
        public int MotherBoardNothernBridgeId { get; set; }
        [Display(Name = "Чіп-сет")]
        public string MotherBoardNothernBridge { get; set; }



        [Required(ErrorMessage = "Поле обов'язкове")]
        [Display(Name = "Сокет процесора")]
        public int CPUSocketId { get; set; }
        [Display(Name = "Сокет процесора")]
        public string CPUSocket { get; set; }


        [Display(Name = "Слоти під ОЗП")]
        public List<RAMTypeViewModel> MotherBoardRAMSlots { get; set; }

        [Display(Name = "Інтерфейси для блока живлення")]
        public List<PowerSupplyMotherBoardInterfaceViewModel> PowerSupplyMotherBoardInterfaces { get; set; }

        [Display(Name = "Інтерфейси для зовнішньої памя'ті")]
        public List<OuterMemoryInterfaceViewModel> OuterMemoryInterfaces { get; set; }

        [Display(Name = "Інтерфйеси для відеокарти")]
        public List<VideoCardInterfaceViewModel> VideoCardInterfaces { get; set; }

    }
}
