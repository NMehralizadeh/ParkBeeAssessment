using System.Collections.Generic;

namespace ParkBee.Assessment.Domain.Entities
{
    public class Garage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public ICollection<Door> Doors { get; set; }
    }
}