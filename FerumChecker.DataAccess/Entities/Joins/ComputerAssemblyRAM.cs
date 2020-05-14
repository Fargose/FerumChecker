﻿using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.DataAccess.Entities.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FerumChecker.DataAccess.Entities.Joins
{
    public class ComputerAssemblyRAM
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [ForeignKey("ComputerAssembly")]
        public int ComputerAssemblyId { get; set; }
        public ComputerAssembly ComputerAssembly { get; set; }


        [Required]
        [ForeignKey("RAM")]
        public int RAMId { get; set; }
        public RAM RAM { get; set; }
    }
}