using System.Diagnostics.CodeAnalysis;

namespace Tradestands_Hags.Emails.Configuration;

[ExcludeFromCodeCoverage]
public class MailSettings
{
    public string SmtpServer { get; set; } = null!;
    public int SmtpPort { get; set; }
    public string ImapServer { get; set; } = null!;
    public int ImapPort { get; set; }
    public string SenderName { get; set; } = null!;
    public string EmailAddress { get; set; } = null!;
    public string Password { get; set; } = null!;
}