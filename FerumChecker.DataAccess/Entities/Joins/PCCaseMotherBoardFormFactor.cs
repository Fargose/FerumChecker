using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.DataAccess.Entities.Specification;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FerumChecker.DataAccess.Entities.Joins
{
    public class PCCaseMotherBoardFormFactor
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Поле обов'язкове")]
        [ForeignKey("PCCase")]
        public int PCCaseId { get; set; }
        public PCCase PCCase { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [ForeignKey("MotherBoardFormFactor")]
        public int MotherBoardFormFactorId { get; set; }
        public MotherBoardFormFactor MotherBoardFormFactor { get; set; }
    }
}
