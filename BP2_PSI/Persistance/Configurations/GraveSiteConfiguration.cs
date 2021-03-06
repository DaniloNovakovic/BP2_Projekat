﻿using Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Persistance.Configurations
{
    public class GraveSiteConfiguration : EntityTypeConfiguration<GraveSite>
    {
        public GraveSiteConfiguration()
        {
            HasKey(gs => gs.DeathRecordId);

            HasRequired(gs => gs.DeathRecord)
                .WithOptional()
                .WillCascadeOnDelete();

            Property(gs => gs.Type).IsRequired();
        }
    }
}