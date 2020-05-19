using System;

namespace Core.Entities
{
    public class Contract
    {
        public long Id { get; set; }

        public long FamilyMemberId { get; set; }
        public FamilyMember FamilyMember { get; set; }

        public long ManagerId { get; set; }
        public Manager Manager { get; set; }

        public long ChapelId { get; set; }
        public Chapel Chapel { get; set; }

        public FuneralType FuneralType { get; set; }
        public DateTime? FuneralStartTime { get; set; }
    }
}