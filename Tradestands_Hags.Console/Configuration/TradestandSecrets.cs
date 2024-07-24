using Tradestands_Hags.Emails.Configuration;

namespace Tradestands_Hags.Console.Configuration;

public abstract class TradestandSecrets
{
    public MailSettings MailSettings { get; set; } = null!;
}