using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.DataAccess.Entities.Infrastructure;
using FerumChecker.DataAccess.Entities.Specification;
using FerumChecker.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Service.Interfaces.Hardware
{
    public interface IPublisherService
    {
        Publisher GetPublisher(int? id);
        IEnumerable<Publisher> GetPublishers();

        OperationDetails CreatePublisher(Publisher publisher);

        OperationDetails UpdatePublisher(Publisher publisher);

        OperationDetails DeletePublisher(int? id);
        void Dispose();
    }
}
