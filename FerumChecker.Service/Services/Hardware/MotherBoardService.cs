using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.Repository.Interfaces;
using FerumChecker.Service.Infrastructure;
using FerumChecker.Service.Interfaces.Hardware;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Service.Services.Hardware
{
    public class MotherBoardService : IMotherBoardService
    {

        IUnitOfWork Database { get; set; }

        public MotherBoardService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public MotherBoard GetMotherBoard(int? id)
        {
            return Database.MotherBoards.Get(id.Value);
        }

        public IEnumerable<MotherBoard> GetMotherBoards()
        {
            return Database.MotherBoards.GetAll();
        }

        public OperationDetails UpdateMotherBoard(MotherBoard motherBoard)
        {
            
            Database.MotherBoards.Update(motherBoard);
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails CreateMotherBoard(MotherBoard motherBoard)
        {
            Database.MotherBoards.Create(motherBoard);
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails DeleteMotherBoard(int? id)
        {
            Database.MotherBoards.Delete(id.Value);
            return new OperationDetails(true, "Ok", "");
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
