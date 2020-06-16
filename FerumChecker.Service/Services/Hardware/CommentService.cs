using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.DataAccess.Entities.User;
using FerumChecker.Repository.Interfaces;
using FerumChecker.Service.Infrastructure;
using FerumChecker.Service.Interfaces.Hardware;
using FerumChecker.Service.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Service.Services.Hardware
{
    public class CommentService : ICommentService
    {

        IUnitOfWork Database { get; set; }
        IComputerAssemblyService _computerAssemblyService { get; set; }
        public CommentService(IUnitOfWork uow, IComputerAssemblyService computerAssemblyService)
        {
            Database = uow;
            _computerAssemblyService = computerAssemblyService;
        }

        public Comment GetComment(int? id)
        {
            return Database.Comments.Get(id.Value);
        }

        public IEnumerable<Comment> GetComments()
        {
            return Database.Comments.GetAll();
        }

        public OperationDetails UpdateComment(Comment cpu)
        {
            
            Database.Comments.Update(cpu);
            Database.Save();
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails CreateComment(Comment cpu)
        {
            Database.Comments.Create(cpu);
            Database.Save();
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails DeleteComment(int? id)
        {
            Database.Comments.Delete(id.Value);
            Database.Save();
            return new OperationDetails(true, "Ok", "");
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
