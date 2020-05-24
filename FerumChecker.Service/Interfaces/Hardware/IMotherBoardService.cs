using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Service.Interfaces.Hardware
{
    public interface IMotherBoardService
    {
        MotherBoard GetMotherBoard(int? id);
        IEnumerable<MotherBoard> GetMotherBoards();

        OperationDetails CreateMotherBoard(MotherBoard motherBoard);

        OperationDetails UpdateMotherBoard(MotherBoard motherBoard);

        OperationDetails DeleteMotherBoard(int? id);

        void Dispose();
    }
}
