using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.DataAccess.Entities.Specification;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FerumChecker.DataAccess.Entities.Joins
{
    public class PCCaseOuterMemoryFormFactor
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Поле обов'язкове")]
        [ForeignKey("PCCase")]
        public int PCCaseId { get; set; }
        public PCCase PCCase { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [ForeignKey("OuterMemoryFormFactor")]
        public int OuterMemoryFormFactorId { get; set; }
        public OuterMemoryFormFactor OuterMemoryFormFactors { get; set; }
    }
}
