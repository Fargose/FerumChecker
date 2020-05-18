using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.DataAccess.Entities.Specification;
using FerumChecker.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Service.Interfaces.Hardware
{
    public interface IVideoCardInterfaceService
    {
        VideoCardInterface GetVideoCardInterface(int? id);
        IEnumerable<VideoCardInterface> GetVideoCardInterfaces();

        OperationDetails CreateVideoCardInterface(VideoCardInterface videoCardInterface);

        OperationDetails UpdateVideoCardInterface(VideoCardInterface videoCardInterface);

        OperationDetails DeleteVideoCardInterface(int? id);
        void Dispose();
    }
}
