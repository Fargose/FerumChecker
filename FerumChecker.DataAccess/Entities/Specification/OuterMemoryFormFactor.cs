using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FerumChecker.DataAccess.Entities.Specification
{
    public class OuterMemoryFormFactor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength]
        public int Name { get; set; }
    }
}
