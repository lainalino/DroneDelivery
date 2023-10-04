using DroneDelivery.Domain.Models;
using DroneDelivery.Services.Interfaces;

namespace DroneDelivery.Services
{
    public class TripCombinationService : ITripCombinationService
    {
        private readonly string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Split("\\bin")[0], @"Files\Input.txt");
        private readonly IKnapSackService _knapSackService;

        public TripCombinationService(IKnapSackService knapSackService)
        {
            _knapSackService = knapSackService;
        }

        /// <summary> 
        /// The code fill the location and the drones. After it, get the fewest number of trips using KnapSack Algorithm
        /// So write the result in console
        /// </summary>
        public void ProcessDelivery()
        {
            if (File.Exists(_filePath))
            {
                string[] lines = File.ReadAllLines(_filePath);

                //Fill the informations about Location
                var locations = FillLocations(lines.Skip(1).ToArray());

                //Fill the informations about Location
                var drones = FillDrone(lines[0]);

                if (locations.Count() > 0 && drones.Count <= 100)
                {
                    // Get the fewest number of trips
                    var trips = GetTrips(drones, locations);

                    //Write the result in console
                    WriteResult(trips);
                }
            }
        }

        /// <summary> 
        /// Fill drone from Input.txt
        /// </summary>
        private List<Drone> FillDrone(string lineDroneDefinition)
        {
            var dronesLine = lineDroneDefinition.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            List<Drone> drones = new List<Drone>();

            for (int i = 0; i < dronesLine.Length - 1; i += 2)
            {
                drones.Add(new Drone
                {
                    Name = dronesLine[i].Trim('[', ']'),
                    MaxWeight = int.Parse(dronesLine[i + 1].Trim('[', ']'))
                });
            }

            return drones;
        }

        /// <summary> 
        /// Fill Locations from Input.txt
        /// </summary>
        private List<Location> FillLocations(string[] lineLocations)
        {
            var locations = new List<Location>();

            foreach (var loc in lineLocations)
            {
                var location = loc.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                locations.Add(new Location
                {
                    Name = location[0].Trim('[', ']'),
                    PackageWeight = int.Parse(location[1].Trim('[', ']'))
                });
            }

            return locations;
        }

        /// <summary> 
        /// Get the fewest number of trips 
        /// </summary>
        private List<(Drone Drone, IList<Location> Locations)> GetTrips(List<Drone> listDrones, List<Location> listLocations)
        {
            var result = new List<(Drone Drone, IList<Location> Trip)>();

            while (listLocations.Count() > 0)
            {
                var maxCombinations = new Dictionary<Drone, Trip>();

                // for each drone get the maxCombinations
                foreach (var drone in listDrones)
                {
                    var solution = _knapSackService.Solve(listLocations.ToArray(), drone.MaxWeight);
                    maxCombinations.Add(drone, solution);
                }

                // order the combinations taking the UnusedCapacity into considerations
                var orderedSolutions = maxCombinations
                      .OrderByDescending(x => x.Value.Locations.Count)
                      .ThenBy(y => y.Value.UnusedCapacity)
                      .ToList();

                var tripOrder = orderedSolutions[0];

                //Add the information in case it is not on the List 
                if (!result.Contains((tripOrder.Key, tripOrder.Value.Locations)))
                    result.Add((tripOrder.Key, tripOrder.Value.Locations));

                foreach (var location in tripOrder.Value.Locations)
                    listLocations.Remove(location);
            }
            return result;
        }

        /// <summary> 
        /// Write the result on console
        /// </summary>
        private void WriteResult(List<(Drone Drone, IList<Location> Locations)> trips)
        {
            trips = trips.OrderBy(x => x.Drone.MaxWeight).ToList();
            string nomeDrone = string.Empty;
            int i = 1;
            foreach (var row in trips)
            {
                if(!nomeDrone.Equals(row.Drone.Name))
                {
                    i = 1;
                    nomeDrone = row.Drone.Name;
                    Console.WriteLine(string.Empty);
                    Console.WriteLine(row.Drone.Name);
                }
                Console.WriteLine("Trip #" + i++);
                Console.WriteLine(string.Join(", ", row.Locations.Select(x => x.Name).ToArray()));

            }
        }
    }
}
