# Drone Delivery

##Overview

A squad of drones is tasked with delivering packages for a major online reseller in a world where time and distance do not matter. Each drone can carry a specific weight and 
can make multiple deliveries before returning to home base to pick up additional loads; however, the goal is to make the fewest number of trips as each time the drone returns 
to home base, it is extremely costly to refuel and reload the drone.

### **Stacks**:

- The implementation is developed in C#
- Use of the .Net 7 framework
- Visual Studio 2022
- Packages: Microsoft.Extensions.DependencyInjection

### ** Drone Delivery**:

There are two projects: 
- DroneDelivery.Domain where is the domains of the projects
- DroneDelivery where the solution is implemented

The **AllCombinations** method of the **KnapSackService** class implements the KnapSack Algorithm. The KnapSack problem is an example of a combinational optimization problem. 
The **ProcessDelivery** method of the **TripCombinationService** class implements the code to get the informations from an input text ( this File must to be in ./Files with the name Input.txt), read the information about the Drones and Locations. With these informations, in the method **GetTrips**, the knapSack algorithms is called.
In each localization iteration, each drone's knapsack will be run and the solution that returns the greatest number of locations will be selected. At the end of each iteration, the locations selected by the drone from the iteration locations list are removed until there are no locations left.
The result is displayed in the console
