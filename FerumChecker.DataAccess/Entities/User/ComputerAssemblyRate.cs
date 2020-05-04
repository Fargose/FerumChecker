using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FerumChecker.DataAccess.Entities.User
{
    public class ComputerAssemblyRate
    {
        [Key]
        public int Id { get; set; }


        [MinLength(0)]
        [MaxLength(5)]
        public short Rate { get; set; }

        [Required]
        [ForeignKey("UserProfile")]
        public string OwnerId { get; set; }

        public UserProfile Owner { get; set; }

        [Required]
        [ForeignKey("ComputerAssembly")]
        public int ComputerAssemblyId { get; set; }

        public ComputerAssembly ComputerAssemblies { get; set; }
    }
}
