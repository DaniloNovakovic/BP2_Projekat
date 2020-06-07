namespace Persistance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedGraveSiteIdInDeathRecordToNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DeathRecords", "GraveSiteId", c => c.Long());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DeathRecords", "GraveSiteId", c => c.Long(nullable: false));
        }
    }
}
