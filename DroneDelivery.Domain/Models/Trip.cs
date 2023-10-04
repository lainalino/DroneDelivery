namespace DroneDelivery.Domain.Models
{
    public class Trip
    {
        public List<Location> Locations { get; set; } = new List<Location>();
        public int UnusedCapacity { get; set; }
    }

}
