using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.DataAccess.Entities.Infrastructure;
using FerumChecker.DataAccess.Entities.Specification;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FerumChecker.DataAccess.Entities.Joins
{
    public class SoftwareVideoCardRequirement
    {
        public int? Id { get; set; }

        [Required]
        [ForeignKey("Software")]
        public int SoftwareId { get; set; }
        public Software Software { get; set; }


        [Required]
        [ForeignKey("VideoCard")]
        public int VideoCardId { get; set; }
        public VideoCard VideoCard { get; set; }


        [Required]
        [ForeignKey("RequirementType")]
        public int RequirementTypeId { get; set; }
        public RequirementType RequirementType { get; set; }

    }
}
