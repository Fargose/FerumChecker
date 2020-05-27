using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.DataAccess.Entities.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FerumChecker.DataAccess.Entities.Joins
{
    public class ComputerAssemblyHDD
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Поле обов'язкове")]
        [ForeignKey("ComputerAssembly")]
        public int ComputerAssemblyId { get; set; }
        public ComputerAssembly ComputerAssembly { get; set; }


        [Required(ErrorMessage = "Поле обов'язкове")]
        [ForeignKey("HDD")]
        public int HDDId { get; set; }
        public HDD HDD { get; set; }
    }
}
