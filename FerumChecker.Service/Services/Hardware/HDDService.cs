﻿using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.Repository.Interfaces;
using FerumChecker.Service.Infrastructure;
using FerumChecker.Service.Interfaces.Hardware;
using FerumChecker.Service.Interfaces.Infrastructure;
using FerumChecker.Service.Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Service.Services.Hardware
{
    public class HDDService : IHDDService
    {

        IUnitOfWork Database { get; set; }

        IComputerAssemblyService _computerAssemblyService { get; set; }
        public HDDService(IUnitOfWork uow, IComputerAssemblyService computerAssemblyService)
        {
            Database = uow;
            _computerAssemblyService = computerAssemblyService;
        }

        public HDD GetHDD(int? id)
        {
            return Database.HDDs.Get(id.Value);
        }

        public IEnumerable<HDD> GetHDDs()
        {
            return Database.HDDs.GetAll();
        }

        public OperationDetails UpdateHDD(HDD hdd)
        {
            
            Database.HDDs.Update(hdd);
            Database.Save();
            hdd = GetHDD(hdd.Id);
            _computerAssemblyService.OnHDDChange(hdd);
            Database.Save();

            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails CreateHDD(HDD hdd)
        {
            Database.HDDs.Create(hdd);
            Database.Save();

            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails DeleteHDD(int? id)
        {
            _computerAssemblyService.OnHDDDelete(id.Value);
            Database.HDDs.Delete(id.Value);
            Database.Save();

            return new OperationDetails(true, "Ok", "");
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
