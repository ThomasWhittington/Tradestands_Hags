using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tradestands_Hags.Console;
using Tradestands_Hags.Console.Configuration;
using Tradestands_Hags.Emails.Configuration;
using Tradestands_Hags.Emails.Services;

IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json", false)
    .AddUserSecrets<TradestandSecrets>()
    .Build();

var serviceProvider = new ServiceCollection()
    .Configure<MailSettings>(config.GetSection(nameof(MailSettings)))
    .AddTransient<IMailService, MailService>()
    .AddTransient<ConsoleManager>()
    .BuildServiceProvider();

var service = serviceProvider.GetRequiredService<ConsoleManager>();
service.Run();