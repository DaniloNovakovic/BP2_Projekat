using System;

namespace Core.Entities
{
    public class Contract
    {
        public long Id { get; set; }

        public FamilyMember FamilyMember { get; set; }
        public Manager Manager { get; set; }
        public Chapel Chapel { get; set; }

        public FuneralType FuneralType { get; set; }
        public DateTime? FuneralStartTime { get; set; }
    }
}