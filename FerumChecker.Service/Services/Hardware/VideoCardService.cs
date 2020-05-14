using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.Repository.Interfaces;
using FerumChecker.Service.Infrastructure;
using FerumChecker.Service.Interfaces.Hardware;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Service.Services.Hardware
{
    public class VideoCardService : IVideoCardService
    {

        IUnitOfWork Database { get; set; }

        public VideoCardService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public VideoCard GetVideoCard(int? id)
        {
            return Database.VideoCards.Get(id.Value);
        }

        public IEnumerable<VideoCard> GetVideoCards()
        {
            return Database.VideoCards.GetAll();
        }

        public OperationDetails UpdateVideoCard(VideoCard videoCard)
        {
            
            Database.VideoCards.Update(videoCard);
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails CreateVideoCard(VideoCard videoCard)
        {
            Database.VideoCards.Create(videoCard);
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails DeleteVideoCard(int? id)
        {
            Database.VideoCards.Delete(id.Value);
            return new OperationDetails(true, "Ok", "");
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
