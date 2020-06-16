namespace Persistance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddSelectChapelStoredProcedure : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure(
                "SelectChapelById",
                p => new
                {
                    id = p.Int()
                },
                @"SELECT * FROM Chapels LEFT OUTER JOIN Locations ON Chapels.LocationId = Locations.Id WHERE Chapels.Id = @id"
              );
        }

        public override void Down()
        {
            DropStoredProcedure("SelectChapelById");
        }
    }
}