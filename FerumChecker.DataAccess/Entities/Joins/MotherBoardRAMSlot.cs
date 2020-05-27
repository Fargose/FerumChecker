using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.DataAccess.Entities.Specification;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FerumChecker.DataAccess.Entities.Joins
{
    public class MotherBoardRAMSlot
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Поле обов'язкове")]
        [ForeignKey("MotherBoard")]
        public int MotherBoardId { get; set; }
        public MotherBoard MotherBoard { get; set; }

        public int ChannelsCount{ get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [ForeignKey("RAMType")]
        public int RAMTypeId { get; set; }
        public RAMType RAMType { get; set; }
    }
}
