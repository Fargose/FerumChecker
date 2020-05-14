using FerumChecker.DataAccess.Entities.Hardware;
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

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        [ForeignKey("MotherBoard")]
        public int MotherBoardId { get; set; }

        public MotherBoard MotherBoard { get; set; }

        [Required]
        [ForeignKey("CPU")]
        public int CPUId { get; set; }

        public CPU CPU { get; set; }


        [Required]
        [ForeignKey("PCCase")]
        public int PCCaseId { get; set; }

        public PCCase PCCase { get; set; }


        [Required]
        [ForeignKey("PowerSupply")]
        public int PowerSupplyId { get; set; }

        public PowerSupply PowerSupply { get; set; }



        [Required]
        [ForeignKey("UserProfile")]
        public string OwnerId { get; set; }

        public UserProfile Owner { get; set; }

        public ICollection<RAM> RAMs { get; } = new List<RAM>();
        public ICollection<SSD> SSDs { get; } = new List<SSD>();
        public ICollection<HDD> HDDs { get; } = new List<HDD>();
        public ICollection<VideoCard> VideoCards { get; } = new List<VideoCard>();

        public ICollection<Comment> Comments { get; } = new List<Comment>();
        public ICollection<ComputerAssemblyRate> ComputerAssemblyRates { get; } = new List<ComputerAssemblyRate>();


    }
}
