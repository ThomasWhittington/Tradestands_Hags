using Tradestands_Hags.Emails.Configuration;
using Tradestands_Hags.GoogleSheets.Configuration;

namespace Tradestands_Hags.Console.Configuration;

public abstract class TradestandSecrets
{
    public MailSettings MailSettings { get; set; } = null!;
    public GoogleSheetsSecrets GoogleSheetsSecrets { get; set; } = null!;
}