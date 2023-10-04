using DroneDelivery.Services;
using Microsoft.Extensions.DependencyInjection;
using DroneDelivery.Services.Interfaces;

//Dependency Injection
var serviceCollection = new ServiceCollection();
serviceCollection.AddScoped<ITripCombinationService, TripCombinationService>();
serviceCollection.AddScoped<IKnapSackService, KnapSackService>();
var serviceProvider = serviceCollection.BuildServiceProvider();

var _deliveryCombinationService = serviceProvider.GetRequiredService<ITripCombinationService>();

//run the code to get the fewest number of trips and write the result on console
_deliveryCombinationService.ProcessDelivery();
