using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.DataAccess.Entities.Infrastructure;
using FerumChecker.DataAccess.Entities.Specification;
using FerumChecker.Repository.Interfaces;
using FerumChecker.Service.Infrastructure;
using FerumChecker.Service.Interfaces.Hardware;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Service.Services.Hardware
{
    public class PublisherService : IPublisherService
    {

        IUnitOfWork Database { get; set; }

        public PublisherService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public Publisher GetPublisher(int? id)
        {
            return Database.Publishers.Get(id.Value);
        }

        public IEnumerable<Publisher> GetPublishers()
        {
            return Database.Publishers.GetAll();
        }

        public OperationDetails UpdatePublisher(Publisher publisher)
        {
            
            Database.Publishers.Update(publisher);
            Database.Save();
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails CreatePublisher(Publisher publisher)
        {
            Database.Publishers.Create(publisher);
            Database.Save();
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails DeletePublisher(int? id)
        {
            Database.Publishers.Delete(id.Value);
            Database.Save();
            return new OperationDetails(true, "Ok", "");
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
