using DroneDelivery.Domain.Models;

namespace DroneDelivery.Services.Interfaces
{
    public interface IKnapSackService
    {
        Trip Solve(Location[] location, int weightCapacity);
    }
}
