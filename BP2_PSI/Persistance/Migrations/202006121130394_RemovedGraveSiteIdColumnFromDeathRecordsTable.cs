namespace Persistance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedGraveSiteIdColumnFromDeathRecordsTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.DeathRecords", "GraveSiteId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DeathRecords", "GraveSiteId", c => c.Long());
        }
    }
}
