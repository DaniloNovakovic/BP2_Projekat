namespace Persistance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Chapels",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        LocationId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locations", t => t.LocationId, cascadeDelete: true)
                .Index(t => t.LocationId);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Coffins",
                c => new
                    {
                        GraveSiteId = c.Long(nullable: false),
                        Mark = c.String(),
                    })
                .PrimaryKey(t => t.GraveSiteId)
                .ForeignKey("dbo.GraveSites", t => t.GraveSiteId, cascadeDelete: true)
                .Index(t => t.GraveSiteId);
            
            CreateTable(
                "dbo.GraveSites",
                c => new
                    {
                        DeathRecordId = c.Long(nullable: false),
                        Type = c.String(nullable: false),
                        LocationId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.DeathRecordId)
                .ForeignKey("dbo.DeathRecords", t => t.DeathRecordId, cascadeDelete: true)
                .ForeignKey("dbo.Locations", t => t.LocationId, cascadeDelete: true)
                .Index(t => t.DeathRecordId)
                .Index(t => t.LocationId);
            
            CreateTable(
                "dbo.DeathRecords",
                c => new
                    {
                        PersonId = c.Long(nullable: false),
                        DeathDate = c.DateTime(),
                        GraveSiteId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.PersonId)
                .ForeignKey("dbo.People", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.PersonId);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        BirthDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Contracts",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FamilyMemberId = c.Long(nullable: false),
                        ManagerId = c.Long(nullable: false),
                        ChapelId = c.Long(nullable: false),
                        FuneralType = c.Int(nullable: false),
                        FuneralStartTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Chapels", t => t.ChapelId, cascadeDelete: true)
                .ForeignKey("dbo.FamilyMembers", t => t.FamilyMemberId, cascadeDelete: true)
                .ForeignKey("dbo.Managers", t => t.ManagerId, cascadeDelete: true)
                .Index(t => t.FamilyMemberId)
                .Index(t => t.ManagerId)
                .Index(t => t.ChapelId);
            
            CreateTable(
                "dbo.FamilyMembers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RelatedToId = c.Long(nullable: false),
                        MemberId = c.Long(nullable: false),
                        RelationType = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.MemberId)
                .ForeignKey("dbo.DeathRecords", t => t.RelatedToId)
                .Index(t => t.RelatedToId)
                .Index(t => t.MemberId);
            
            CreateTable(
                "dbo.Managers",
                c => new
                    {
                        WorkerId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.WorkerId)
                .ForeignKey("dbo.Workers", t => t.WorkerId, cascadeDelete: true)
                .Index(t => t.WorkerId);
            
            CreateTable(
                "dbo.TechnicalStaffs",
                c => new
                    {
                        WorkerId = c.Long(nullable: false),
                        ManagerId = c.Long(),
                    })
                .PrimaryKey(t => t.WorkerId)
                .ForeignKey("dbo.Workers", t => t.WorkerId, cascadeDelete: true)
                .ForeignKey("dbo.Managers", t => t.ManagerId)
                .Index(t => t.WorkerId)
                .Index(t => t.ManagerId);
            
            CreateTable(
                "dbo.Workers",
                c => new
                    {
                        PersonId = c.Long(nullable: false),
                        WorkTime = c.String(),
                        Role = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PersonId)
                .ForeignKey("dbo.People", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.PersonId);
            
            CreateTable(
                "dbo.Urns",
                c => new
                    {
                        GraveSiteId = c.Long(nullable: false),
                        Capacity = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.GraveSiteId)
                .ForeignKey("dbo.GraveSites", t => t.GraveSiteId, cascadeDelete: true)
                .Index(t => t.GraveSiteId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Urns", "GraveSiteId", "dbo.GraveSites");
            DropForeignKey("dbo.Contracts", "ManagerId", "dbo.Managers");
            DropForeignKey("dbo.Managers", "WorkerId", "dbo.Workers");
            DropForeignKey("dbo.TechnicalStaffs", "ManagerId", "dbo.Managers");
            DropForeignKey("dbo.TechnicalStaffs", "WorkerId", "dbo.Workers");
            DropForeignKey("dbo.Workers", "PersonId", "dbo.People");
            DropForeignKey("dbo.Contracts", "FamilyMemberId", "dbo.FamilyMembers");
            DropForeignKey("dbo.FamilyMembers", "RelatedToId", "dbo.DeathRecords");
            DropForeignKey("dbo.FamilyMembers", "MemberId", "dbo.People");
            DropForeignKey("dbo.Contracts", "ChapelId", "dbo.Chapels");
            DropForeignKey("dbo.Coffins", "GraveSiteId", "dbo.GraveSites");
            DropForeignKey("dbo.GraveSites", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.GraveSites", "DeathRecordId", "dbo.DeathRecords");
            DropForeignKey("dbo.DeathRecords", "PersonId", "dbo.People");
            DropForeignKey("dbo.Chapels", "LocationId", "dbo.Locations");
            DropIndex("dbo.Urns", new[] { "GraveSiteId" });
            DropIndex("dbo.Workers", new[] { "PersonId" });
            DropIndex("dbo.TechnicalStaffs", new[] { "ManagerId" });
            DropIndex("dbo.TechnicalStaffs", new[] { "WorkerId" });
            DropIndex("dbo.Managers", new[] { "WorkerId" });
            DropIndex("dbo.FamilyMembers", new[] { "MemberId" });
            DropIndex("dbo.FamilyMembers", new[] { "RelatedToId" });
            DropIndex("dbo.Contracts", new[] { "ChapelId" });
            DropIndex("dbo.Contracts", new[] { "ManagerId" });
            DropIndex("dbo.Contracts", new[] { "FamilyMemberId" });
            DropIndex("dbo.DeathRecords", new[] { "PersonId" });
            DropIndex("dbo.GraveSites", new[] { "LocationId" });
            DropIndex("dbo.GraveSites", new[] { "DeathRecordId" });
            DropIndex("dbo.Coffins", new[] { "GraveSiteId" });
            DropIndex("dbo.Chapels", new[] { "LocationId" });
            DropTable("dbo.Urns");
            DropTable("dbo.Workers");
            DropTable("dbo.TechnicalStaffs");
            DropTable("dbo.Managers");
            DropTable("dbo.FamilyMembers");
            DropTable("dbo.Contracts");
            DropTable("dbo.People");
            DropTable("dbo.DeathRecords");
            DropTable("dbo.GraveSites");
            DropTable("dbo.Coffins");
            DropTable("dbo.Locations");
            DropTable("dbo.Chapels");
        }
    }
}
