using System;

namespace ParkBee.Assessment.Domain.Entities
{
    public class DoorStatusHistory
    {
        public int Id { get; set; }
        public int DoorId { get; set; }
        public bool IsOnline { get; set; }
        public DateTimeOffset ChangeDate { get; set; }
        public Door Door { get; set; }
    }
}