﻿using FerumChecker.DataAccess.Entities.Infrastructure;
using FerumChecker.DataAccess.Entities.Specification;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FerumChecker.DataAccess.Entities.Hardware
{
    public class CPU
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [MaxLength(200, ErrorMessage = "Некоректне поле")]
        public string Name { get; set; }

        [MaxLength(1000, ErrorMessage = "Некоректне поле")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        public int CoresNumber { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [MaxLength(100, ErrorMessage = "Некоректне поле")]
        public string CoresName { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        public int Frequency { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        public int MaxFrequency { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        public int ThreadsNumber { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public string  Image { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [ForeignKey("CPUSocket")]
        public int CPUSocketId { get; set; }
        public CPUSocket CPUSocket { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [ForeignKey("Manufacturer")]
        public int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }

        public int? Year { get; set; }

        public ICollection<ComputerAssembly> ComputerAssemblies { get; } = new List<ComputerAssembly>();
    }
}
