﻿using Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Persistance.Configurations
{
    public class DeathRecordConfiguration : EntityTypeConfiguration<DeathRecord>
    {
        public DeathRecordConfiguration()
        {
            HasKey(dr => dr.PersonId);

            HasRequired(dr => dr.Person)
                .WithOptional()
                .WillCascadeOnDelete();
        }
    }
}