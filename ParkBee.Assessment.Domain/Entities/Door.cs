using System.Collections.Generic;

namespace ParkBee.Assessment.Domain.Entities
{
    public class Door
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int GarageId { get; set; }
        public string IP { get; set; }      
        public Garage Garage { get; set; }
        public ICollection<DoorStatusHistory> DoorStatusHistories { get; set; }
    }
}