﻿namespace Persistance.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Persistance.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Persistance.ApplicationDbContext context)
        {
            // This method will be called after migrating to the latest version.

            new ApplicationDbBootstrapper(context).Seed();
        }
    }
}