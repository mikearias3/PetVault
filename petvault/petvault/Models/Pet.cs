using System;
using System.ComponentModel;

namespace petvault.Models
{
    public class Pet
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public double LastLatitude { get; set; }
        public double LastLongitude { get; set; }
    }
}
