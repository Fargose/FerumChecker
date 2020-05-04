using FerumChecker.DataAccess.Entities.Hardware;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FerumChecker.DataAccess.Entities.Joins
{
    public class ComputerAssemblySSD
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [ForeignKey("ComputerAssembly")]
        public int ComputerAssemblyId { get; set; }
        public ComputerAssembly ComputerAssembly { get; set; }


        [Required]
        [ForeignKey("SSD")]
        public int SSDId { get; set; }
        public SSD SSD { get; set; }
    }
}
