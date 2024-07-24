using System.Diagnostics.CodeAnalysis;

namespace Tradestands_Hags.GoogleSheets.Configuration;

[ExcludeFromCodeCoverage]
public class GoogleSheetsSecrets
{
    public string SheetId { get; set; } = null!;
    public string CredentialLocation { get; set; } = null!;
    public string ApplicationName { get; set; } = null!;
    public string DataRange { get; set; } = null!;
}