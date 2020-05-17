namespace Persistance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Chapels",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locations", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
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
                        Id = c.Long(nullable: false),
                        Type = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DeathRecords", t => t.Id, cascadeDelete: true)
                .ForeignKey("dbo.Locations", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.DeathRecords",
                c => new
                    {
                        PersonId = c.Long(nullable: false),
                        DeathDate = c.DateTime(),
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
                        FuneralType = c.Int(nullable: false),
                        FuneralStartTime = c.DateTime(),
                        Chapel_Id = c.Long(nullable: false),
                        FamilyMember_Id = c.Long(nullable: false),
                        Manager_WorkerId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Chapels", t => t.Chapel_Id, cascadeDelete: true)
                .ForeignKey("dbo.FamilyMembers", t => t.FamilyMember_Id, cascadeDelete: true)
                .ForeignKey("dbo.Managers", t => t.Manager_WorkerId, cascadeDelete: true)
                .Index(t => t.Chapel_Id)
                .Index(t => t.FamilyMember_Id)
                .Index(t => t.Manager_WorkerId);
            
            CreateTable(
                "dbo.FamilyMembers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RelationType = c.String(),
                        Member_Id = c.Long(),
                        RelatedTo_PersonId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.Member_Id)
                .ForeignKey("dbo.DeathRecords", t => t.RelatedTo_PersonId)
                .Index(t => t.Member_Id)
                .Index(t => t.RelatedTo_PersonId);
            
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
            DropForeignKey("dbo.Contracts", "Manager_WorkerId", "dbo.Managers");
            DropForeignKey("dbo.Managers", "WorkerId", "dbo.Workers");
            DropForeignKey("dbo.TechnicalStaffs", "ManagerId", "dbo.Managers");
            DropForeignKey("dbo.TechnicalStaffs", "WorkerId", "dbo.Workers");
            DropForeignKey("dbo.Workers", "PersonId", "dbo.People");
            DropForeignKey("dbo.Contracts", "FamilyMember_Id", "dbo.FamilyMembers");
            DropForeignKey("dbo.FamilyMembers", "RelatedTo_PersonId", "dbo.DeathRecords");
            DropForeignKey("dbo.FamilyMembers", "Member_Id", "dbo.People");
            DropForeignKey("dbo.Contracts", "Chapel_Id", "dbo.Chapels");
            DropForeignKey("dbo.Coffins", "GraveSiteId", "dbo.GraveSites");
            DropForeignKey("dbo.GraveSites", "Id", "dbo.Locations");
            DropForeignKey("dbo.GraveSites", "Id", "dbo.DeathRecords");
            DropForeignKey("dbo.DeathRecords", "PersonId", "dbo.People");
            DropForeignKey("dbo.Chapels", "Id", "dbo.Locations");
            DropIndex("dbo.Urns", new[] { "GraveSiteId" });
            DropIndex("dbo.Workers", new[] { "PersonId" });
            DropIndex("dbo.TechnicalStaffs", new[] { "ManagerId" });
            DropIndex("dbo.TechnicalStaffs", new[] { "WorkerId" });
            DropIndex("dbo.Managers", new[] { "WorkerId" });
            DropIndex("dbo.FamilyMembers", new[] { "RelatedTo_PersonId" });
            DropIndex("dbo.FamilyMembers", new[] { "Member_Id" });
            DropIndex("dbo.Contracts", new[] { "Manager_WorkerId" });
            DropIndex("dbo.Contracts", new[] { "FamilyMember_Id" });
            DropIndex("dbo.Contracts", new[] { "Chapel_Id" });
            DropIndex("dbo.DeathRecords", new[] { "PersonId" });
            DropIndex("dbo.GraveSites", new[] { "Id" });
            DropIndex("dbo.Coffins", new[] { "GraveSiteId" });
            DropIndex("dbo.Chapels", new[] { "Id" });
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
