using Core.Entities;
using Core.Interfaces.Repositories;
using System;

namespace Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<FamilyMember> FamilyMembers { get; set; }
        IPersonRepository Persons { get; set; }
        IRepository<GraveSite> GraveSites { get; set; }
        IRepository<DeathRecord> DeathRecords { get; set; }
        IRepository<Chapel> Chapels { get; set; }
        IRepository<Coffin> Coffins { get; set; }
        IRepository<Location> Locations { get; set; }
        IRepository<Worker> Workers { get; set; }
        IRepository<Manager> Managers { get; set; }
        IRepository<TechnicalStaff> TechnicalStaff { get; set; }
        IRepository<Contract> Contracts { get; set; }
        IRepository<Urn> Urns { get; set; }

        int SaveChanges();
    }
}