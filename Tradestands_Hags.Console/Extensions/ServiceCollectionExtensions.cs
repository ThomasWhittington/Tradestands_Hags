using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tradestands_Hags.Console.Configuration;
using Tradestands_Hags.Emails.Configuration;
using Tradestands_Hags.Emails.Services;

namespace Tradestands_Hags.Console.Extensions;

public static class ServiceCollectionExtensions
{
    public static T Initialise<T>(this ServiceCollection collection) where T : notnull
    {
        IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json", false)
            .AddUserSecrets<TradestandSecrets>()
            .Build();

        var provider = collection
            .Configure<MailSettings>(config.GetSection(nameof(MailSettings)))
            .AddTransient<IMailService, MailService>()
            .AddTransient<ConsoleManager>()
            .BuildServiceProvider();
        return provider.GetRequiredService<T>();
    }
}