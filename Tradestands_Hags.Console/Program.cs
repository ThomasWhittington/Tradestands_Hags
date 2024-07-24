using Microsoft.Extensions.DependencyInjection;
using Tradestands_Hags.Console;
using Tradestands_Hags.Console.Extensions;

var service = new ServiceCollection().Initialise<ConsoleManager>();
await service.Run();