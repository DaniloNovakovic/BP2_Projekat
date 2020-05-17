namespace Persistance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRequiredConstraintsToFamilyMember : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.FamilyMembers", new[] { "Member_Id" });
            DropIndex("dbo.FamilyMembers", new[] { "RelatedTo_PersonId" });
            RenameColumn(table: "dbo.FamilyMembers", name: "Member_Id", newName: "MemberId");
            RenameColumn(table: "dbo.FamilyMembers", name: "RelatedTo_PersonId", newName: "RelatedToId");
            AlterColumn("dbo.FamilyMembers", "MemberId", c => c.Long(nullable: false));
            AlterColumn("dbo.FamilyMembers", "RelatedToId", c => c.Long(nullable: false));
            CreateIndex("dbo.FamilyMembers", "RelatedToId");
            CreateIndex("dbo.FamilyMembers", "MemberId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.FamilyMembers", new[] { "MemberId" });
            DropIndex("dbo.FamilyMembers", new[] { "RelatedToId" });
            AlterColumn("dbo.FamilyMembers", "RelatedToId", c => c.Long());
            AlterColumn("dbo.FamilyMembers", "MemberId", c => c.Long());
            RenameColumn(table: "dbo.FamilyMembers", name: "RelatedToId", newName: "RelatedTo_PersonId");
            RenameColumn(table: "dbo.FamilyMembers", name: "MemberId", newName: "Member_Id");
            CreateIndex("dbo.FamilyMembers", "RelatedTo_PersonId");
            CreateIndex("dbo.FamilyMembers", "Member_Id");
        }
    }
}
