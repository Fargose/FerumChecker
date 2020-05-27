using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.DataAccess.Entities.Joins;
using FerumChecker.DataAccess.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FerumChecker.DataAccess.Entities.Infrastructure
{
    public class ComputerAssembly
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [ForeignKey("MotherBoard")]
        public int? MotherBoardId { get; set; }

        public MotherBoard MotherBoard { get; set; }

        [ForeignKey("CPU")]
        public int? CPUId { get; set; }

        public CPU CPU { get; set; }


        [ForeignKey("PCCase")]
        public int? PCCaseId { get; set; }

        public PCCase PCCase { get; set; }


        [ForeignKey("PowerSupply")]
        public int? PowerSupplyId { get; set; }

        public PowerSupply PowerSupply { get; set; }



        [Required]
        [ForeignKey("UserProfile")]
        public string OwnerId { get; set; }

        public UserProfile Owner { get; set; }

        public bool Public { get; set; }

        public ICollection<ComputerAssemblyRAM> ComputerAssemblyRAMs { get; set; }
        public ICollection<ComputerAssemblySSD> SSDs { get; set; } 
        public ICollection<ComputerAssemblyHDD> HDDs { get; set; }
        public ICollection<ComputerAssemblyVideoCard> VideoCards { get; set; } 

        public ICollection<Comment> Comments { get; } = new List<Comment>();
        public ICollection<ComputerAssemblyRate> ComputerAssemblyRates { get; } = new List<ComputerAssemblyRate>();


    }
}
