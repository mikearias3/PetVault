using System;
namespace petvault.Models
{
    public class PetPositions
    {
        public string Id { get; set; }
        public DateTime createdAt { get; set; }
        public string PetID { get; set; }
        public string Status { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
