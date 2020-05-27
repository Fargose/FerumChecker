using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.Repository.Interfaces;
using FerumChecker.Service.Infrastructure;
using FerumChecker.Service.Interfaces.Hardware;
using FerumChecker.Service.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Service.Services.Hardware
{
    public class VideoCardService : IVideoCardService
    {

        IUnitOfWork Database { get; set; }
        IComputerAssemblyService _computerAssemblyService { get; set; }
        public VideoCardService(IUnitOfWork uow, IComputerAssemblyService computerAssemblyService)
        {
            Database = uow;
            _computerAssemblyService = computerAssemblyService;
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
            Database.Save();

            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails CreateVideoCard(VideoCard videoCard)
        {
            Database.VideoCards.Create(videoCard);
            Database.Save();

            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails DeleteVideoCard(int? id)
        {
            Database.VideoCards.Delete(id.Value);
            Database.Save();

            return new OperationDetails(true, "Ok", "");
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
