using LiteDB;
using System;
using System.Collections.Generic;

namespace ContactEntrySystem.Models
{
    public class Name
    {
        public string first { get; set; }
        public string middle { get; set; }
        public string last { get; set; }
    }

    public class Address
    {
        public string street { get; set; }
       
        public string city { get; set; }
       
        public string state { get; set; }
       
        public string zip { get; set; }
    }

    public class Phone
    {
        public string number { get; set; }
       
        public string type { get; set; }
    }


    public class ContactSystemViewModel
    {
        [BsonId]
        public Guid Id { get; set; }
        public Name name { get; set; }
        public Address address { get; set; }
        public List<Phone> phone { get; set; }
        public string email { get; set; }
    }
}
