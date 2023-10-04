using DroneDelivery.Domain.Models;
using DroneDelivery.Services.Interfaces;

namespace DroneDelivery.Services
{
    public class KnapSackService : IKnapSackService
    {

        /// <summary> 
        /// The code is about KnapSack algorithm.
        /// In the knapsack problem, you need to pack a set of items, 
        /// with given values and sizes (such as weights or volumes), 
        /// into a container with a maximum capacity . 
        /// If the total size of the items exceeds the capacity, you can't pack them all.
        /// </summary>
        public Trip AllCombinations(Location[] listLocation, int weightCapacity)
        {
            int weight = weightCapacity;
            int locationLength = listLocation.Length;

            var matrix = new Trip[locationLength + 1, weight + 1];
            for (int r = 0; r < weight + 1; r++)
            {
                matrix[0, r] = new Trip();
            }
            for (int c = 0; c < locationLength + 1; c++)
            {
                matrix[c, 0] = new Trip();
            }

            //Go through each item. 
            for (int item = 1; item <= locationLength; item++)
            {
                for (int capacity = 1; capacity <= weight; capacity++)
                {
                    var locations = new List<Location>();
                    locations.AddRange(matrix[item - 1, capacity].Locations);
                    Trip valueWithoutCurrentLocation = new Trip { Locations = locations };
                    Trip valueWithCurrentLocation = new Trip();

                    int weightOfCurr = listLocation[item - 1].PackageWeight;

                    //If the current weight of items is less than the current capacity
                    //then we should see if we add this item to the drone
                    //results in a value greater than that determined for
                    // the previous item in this potential capacity.
                    if (capacity >= weightOfCurr)
                    {
                        valueWithCurrentLocation = new Trip() { Locations = new List<Location> { listLocation[item - 1] } };

                        int remainingCapacity = capacity - weightOfCurr;
                        valueWithCurrentLocation.Locations.AddRange(matrix[item - 1, remainingCapacity].Locations);
                    }

                    matrix[item, capacity] = valueWithCurrentLocation.Locations.Count() > valueWithoutCurrentLocation.Locations.Count ? valueWithCurrentLocation : valueWithoutCurrentLocation;
                }
            }

            matrix[locationLength, weight].UnusedCapacity = weightCapacity - matrix[locationLength, weight].Locations.Sum(x => x.PackageWeight);
            return matrix[locationLength, weight];
        }
    }
}
