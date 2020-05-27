using FerumChecker.DataAccess.Entities.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerumChecker.Web.ViewModel.Infrastructure
{
    public class ComputerAssemblyViewModel
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public string UserId { get; set; }

        public int? CPUId { get; set; }
        public string CPUName { get; set; }
        public string CPUImage { get; set; }

        public int? MotherBoardId { get; set; }
        public string MotherBoardName { get; set; }
        public string MotherBoardImage { get; set; }

        public int? PCCaseId { get; set; }
        public string PCCaseName { get; set; }
        public string  PCCaseImage {get ;set;}

        public int? PowerSupplyId { get; set; }
        public string PowerSupplyName { get; set; }
        public string PowerSupplyImage { get; set; }

        public int RAMFreeSlot { get; set; }

        public string OwnerName { get; set; }

        public int OuterMemoryFreeSlot { get; set; }
        public List<VideoCardShortModel> VideoCards { get; set; }

        public List<RAMShortModel> RAMs { get; set; }

        public List<HDDShortModel> HDDs { get; set; }

        public List<SSDShortModel> SSDs { get; set; }
        public class VideoCardShortModel 
        {
            public VideoCardShortModel(VideoCard videoCard)
            {
                VideoCardId = videoCard.Id;
                VideoCardName = videoCard.Name;
                VideoCardImage = "/Images/VideoCard/" + videoCard.Image;
            }
            public int? VideoCardId { get; set; }
            public string VideoCardName { get; set; }
            public string VideoCardImage { get; set; }
        }


        public class RAMShortModel
        {
            public RAMShortModel(RAM ram)
            {
                RAMId = ram.Id;
                RAMName = ram.Name;
                RAMImage = "/Images/RAM/" + ram.Image;
            }
            public int? RAMId { get; set; }
            public string RAMName { get; set; }
            public string RAMImage { get; set; }
        }

        public class SSDShortModel
        {
            public SSDShortModel(SSD ssd)
            {
                SSDId = ssd.Id;
                SSDName = ssd.Name;
                SSDImage = "/Images/SSD/" + ssd.Image;
            }
            public int? SSDId { get; set; }
            public string SSDName { get; set; }
            public string SSDImage { get; set; }
        }

        public class HDDShortModel
        {
            public HDDShortModel(HDD hdd)
            {
                HDDId = hdd.Id;
                HDDName = hdd.Name;
                HDDImage = "/Images/HDD/" + hdd.Image;
            }
            public int? HDDId { get; set; }
            public string HDDName { get; set; }
            public string HDDImage { get; set; }
        }
    }
}
