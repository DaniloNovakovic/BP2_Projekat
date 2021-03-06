﻿using System;

namespace Core.Entities
{
    public class Person
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; } = DateTime.Now;

        public override string ToString()
        {
            return $"{Id}-{FirstName},{LastName}";
        }
    }
}