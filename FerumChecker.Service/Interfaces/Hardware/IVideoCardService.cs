using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Service.Interfaces.Hardware
{
    public interface IVideoCardService
    {
        VideoCard GetVideoCard(int? id);
        IEnumerable<VideoCard> GetVideoCards();

        OperationDetails CreateVideoCard(VideoCard videoCard);

        OperationDetails UpdateVideoCard(VideoCard videoCard);

        OperationDetails DeleteVideoCard(int? id);
        void Dispose();
    }
}
