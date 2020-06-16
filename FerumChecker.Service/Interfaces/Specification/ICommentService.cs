using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.DataAccess.Entities.Specification;
using FerumChecker.DataAccess.Entities.User;
using FerumChecker.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Service.Interfaces.Hardware
{
    public interface ICommentService
    {
        Comment GetComment(int? id);
        IEnumerable<Comment> GetComments();

        OperationDetails CreateComment(Comment gpu);

        OperationDetails UpdateComment(Comment gpu);

        OperationDetails DeleteComment(int? id);
        void Dispose();
    }
}
