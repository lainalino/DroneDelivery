using DroneDelivery.Domain.Models;

namespace DroneDelivery.Services.Interfaces
{
    public interface IKnapSackService
    {
        Trip AllCombinations(Location[] location, int weightCapacity);
    }
}
