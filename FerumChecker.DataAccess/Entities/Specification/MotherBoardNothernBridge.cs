using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FerumChecker.DataAccess.Entities.Specification
{
    public class MotherBoardNothernBridge
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public int Name { get; set; }
    }
}
