﻿using Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Persistance.Configurations
{
    public class FamilyMemberConfiguration : EntityTypeConfiguration<FamilyMember>
    {
        public FamilyMemberConfiguration()
        {
        }
    }
}