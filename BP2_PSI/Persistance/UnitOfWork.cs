﻿using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Persistance.Repositories;
using System;

namespace Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            FamilyMembers = new Repository<FamilyMember>(context);
            Persons = new PersonRepository(context);
            GraveSites = new Repository<GraveSite>(context);
            DeathRecords = new DeathRecordRepository(context);
            Chapels = new ChapelRepository(context);
            Coffins = new CoffinRepository(context);
            Locations = new Repository<Location>(context);
            Workers = new Repository<Worker>(context);
            Managers = new ManagerRepository(context);
            TechnicalStaff = new TechnicalStaffRepository(context);
            Contracts = new Repository<Contract>(context);
            Urns = new UrnRepository(context);
        }

        public IRepository<Chapel> Chapels { get; set; }
        public IRepository<Coffin> Coffins { get; set; }
        public IRepository<Contract> Contracts { get; set; }
        public IDeathRecordRepository DeathRecords { get; set; }
        public IRepository<FamilyMember> FamilyMembers { get; set; }
        public IRepository<GraveSite> GraveSites { get; set; }
        public IRepository<Location> Locations { get; set; }
        public IRepository<Manager> Managers { get; set; }
        public IPersonRepository Persons { get; set; }
        public IRepository<TechnicalStaff> TechnicalStaff { get; set; }
        public IRepository<Urn> Urns { get; set; }
        public IRepository<Worker> Workers { get; set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}