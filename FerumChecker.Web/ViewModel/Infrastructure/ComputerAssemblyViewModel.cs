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
        public string CPUImage {get ;set;}

        public int? MotherBoardId { get; set; }
        public string MotherBoardName { get; set; }
        public string MotherBoardImage { get; set; }

        public List<VideoCardShortModel> VideoCards { get; set; }
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
    }
}
