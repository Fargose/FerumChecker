using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.DataAccess.Entities.Specification;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FerumChecker.DataAccess.Entities.Joins
{
    public class MotherBoardPowerSupplySlot
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [ForeignKey("MotherBoard")]
        public int MotherBoardId { get; set; }
        public MotherBoard MotherBoard { get; set; }


        [Required]
        [ForeignKey("PowerSupplyMotherBoardInterface")]
        public int PowerSupplyMotherBoardInterfaceId { get; set; }
        public PowerSupplyMotherBoardInterface PowerSupplyMotherBoardInterface { get; set; }
    }
}
