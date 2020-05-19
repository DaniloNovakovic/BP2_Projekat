using Core.Entities;
using Core.Enumerations;
using System;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;

namespace Persistance
{
    public class ApplicationDbBootstrapper
    {
        private readonly ApplicationDbContext _context;

        public ApplicationDbBootstrapper(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates Database if not exist and seeds data if needed
        /// </summary>
        public void Initialize()
        {
            Debug.WriteLine("Starting Up database...");
            Debug.WriteLine("Creating Database if it does not exist...");

            _context.Database.CreateIfNotExists();

            Debug.WriteLine("Seeding data...");
            /*
            if (!_context.Managers.Any())
            {
                Seed();
                _context.SaveChanges();
            }*/
        }

        /// <summary>
        /// Seeds the data without calling .SaveChanges
        /// </summary>
        public void Seed()
        {
            // Manager
            var managerPerson = new Person
            {
                Id = 0,
                FirstName = "fn_manager",
                LastName = "ln_manager",
                BirthDate = new DateTime(1969, 1, 3)
            };

            var managerWorker = new Worker
            {
                PersonId = managerPerson.Id,
                Person = managerPerson,
                WorkTime = "9h - 17h",
                Role = WorkerRoles.Manager
            };
            var manager = new Manager
            {
                WorkerId = managerWorker.PersonId,
                Worker = managerWorker
            };

            // Tech staff
            var techStaffPerson = new Person
            {
                Id = 1,
                FirstName = "fn_staff_member",
                LastName = "ln_staff_member",
                BirthDate = new DateTime(1962, 5, 9)
            };

            var techStaffWorker = new Worker
            {
                PersonId = techStaffPerson.Id,
                Person = techStaffPerson,
                WorkTime = "8h - 16h",
                Role = WorkerRoles.TechnicalStaff
            };

            // Dead person
            var deadPerson = new Person
            {
                Id = 2,
                BirthDate = new DateTime(1997, 4, 3),
                FirstName = "Danilo",
                LastName = "Novakovic"
            };

            var deathRecord = new DeathRecord
            {
                PersonId = deadPerson.Id,
                Person = deadPerson,
                DeathDate = DateTime.Now
            };

            // Family
            var sisterPerson = new Person
            {
                Id = 3,
                BirthDate = new DateTime(1999, 2, 6),
                FirstName = "Aleksandra",
                LastName = "Novakovic"
            };

            var familyMember = new FamilyMember
            {
                Id = sisterPerson.Id,
                MemberId = sisterPerson.Id,
                Member = sisterPerson,
                RelatedToId = deathRecord.PersonId,
                RelatedTo = deathRecord,
                RelationType = "sister"
            };

            // Chapel
            var chapelLocation = new Location
            {
                Id = 0,
                Latitude = 45.571988,
                Longitude = 19.668292,
            };

            var chapel = new Chapel
            {
                Id = 0,
                Name = "White Chapel",
                Location = chapelLocation
            };

            // Contracts
            var contract = new Contract
            {
                Id = 0,
                Manager = manager,
                FamilyMember = familyMember,
                FuneralStartTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1, 14, 00, 00),
                FuneralType = FuneralType.Burial,
                Chapel = chapel
            };

            var graveSiteLocation = new Location
            {
                Id = 1,
                Latitude = 45.572393,
                Longitude = 19.667079
            };

            var graveSite = new GraveSite
            {
                Id = 0,
                DeathRecord = deathRecord,
                Location = graveSiteLocation,
                Type = GraveSiteType.Coffin
            };

            var coffin = new Coffin
            {
                GraveSiteId = graveSite.Id,
                GraveSite = graveSite,
                Mark = "Black Coffin v1.0 - Smooth Edition"
            };

            _context.Locations.AddOrUpdate(chapelLocation, graveSiteLocation);
            _context.Chapels.AddOrUpdate(chapel);

            _context.Persons.AddOrUpdate(managerPerson, techStaffPerson, deadPerson, sisterPerson);

            _context.DeathRecords.AddOrUpdate(deathRecord);
            _context.GraveSites.AddOrUpdate(graveSite);
            _context.Coffins.AddOrUpdate(coffin);

            _context.FamilyMembers.AddOrUpdate(familyMember);

            _context.Workers.AddOrUpdate(managerWorker, techStaffWorker);
            _context.Managers.AddOrUpdate(manager);
            _context.TechnicalStaff.AddOrUpdate(new TechnicalStaff
            {
                WorkerId = techStaffWorker.PersonId,
                ManagerId = manager.WorkerId
            });

            _context.Contracts.AddOrUpdate(contract);
        }
    }
}