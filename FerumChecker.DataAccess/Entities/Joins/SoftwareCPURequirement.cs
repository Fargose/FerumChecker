﻿using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.DataAccess.Entities.Infrastructure;
using FerumChecker.DataAccess.Entities.Specification;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FerumChecker.DataAccess.Entities.Joins
{
    public class SoftwareCPURequirement
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [ForeignKey("Software")]
        public int SoftwareId { get; set; }
        public Software Software { get; set; }


        [Required(ErrorMessage = "Поле обов'язкове")]
        [ForeignKey("CPU")]
        public int CPUId { get; set; }
        public CPU CPU { get; set; }


        [Required(ErrorMessage = "Поле обов'язкове")]
        [ForeignKey("RequirementType")]
        public int RequirementTypeId { get; set; }
        public RequirementType RequirementType { get; set; }

    }
}
