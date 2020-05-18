using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.DataAccess.Entities.Specification;
using FerumChecker.Repository.Interfaces;
using FerumChecker.Service.Infrastructure;
using FerumChecker.Service.Interfaces.Hardware;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Service.Services.Hardware
{
    public class VideoCardInterfaceService : IVideoCardInterfaceService
    {

        IUnitOfWork Database { get; set; }

        public VideoCardInterfaceService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public VideoCardInterface GetVideoCardInterface(int? id)
        {
            return Database.VideoCardInterfaces.Get(id.Value);
        }

        public IEnumerable<VideoCardInterface> GetVideoCardInterfaces()
        {
            return Database.VideoCardInterfaces.GetAll();
        }

        public OperationDetails UpdateVideoCardInterface(VideoCardInterface videoCardInterface)
        {
            
            Database.VideoCardInterfaces.Update(videoCardInterface);
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails CreateVideoCardInterface(VideoCardInterface videoCardInterface)
        {
            Database.VideoCardInterfaces.Create(videoCardInterface);
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails DeleteVideoCardInterface(int? id)
        {
            Database.VideoCardInterfaces.Delete(id.Value);
            return new OperationDetails(true, "Ok", "");
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
