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
    public class GraphicMemoryTypeService : IGraphicMemoryTypeService
    {

        IUnitOfWork Database { get; set; }

        public GraphicMemoryTypeService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public GraphicMemoryType GetGraphicMemoryType(int? id)
        {
            return Database.GraphicMemoryTypes.Get(id.Value);
        }

        public IEnumerable<GraphicMemoryType> GetGraphicMemoryTypes()
        {
            return Database.GraphicMemoryTypes.GetAll();
        }

        public OperationDetails UpdateGraphicMemoryType(GraphicMemoryType graphicMemoryType)
        {
            
            Database.GraphicMemoryTypes.Update(graphicMemoryType);
            Database.Save();
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails CreateGraphicMemoryType(GraphicMemoryType graphicMemoryType)
        {
            Database.GraphicMemoryTypes.Create(graphicMemoryType);
            Database.Save();
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails DeleteGraphicMemoryType(int? id)
        {
            Database.GraphicMemoryTypes.Delete(id.Value);
            Database.Save();
            return new OperationDetails(true, "Ok", "");
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
