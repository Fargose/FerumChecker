using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FerumChecker.DataAccess.Entities.Infrastructure
{
    public class Publisher
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        [ForeignKey("Country")]
        public int CountryId { get; set; }

        public Country Country { get; set; }
    }
}
