using System;

namespace Core.Entities
{
    public class Contract
    {
        public FamilyMember FamilyMember { get; set; }
        public Manager Manager { get; set; }
        public Chapel Chapel { get; set; }

        public FuneralType? FuneralType { get; set; }
        public DateTime? FuneralStartTime { get; set; }
    }
}