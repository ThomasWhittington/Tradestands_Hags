using System.Diagnostics.CodeAnalysis;

namespace Tradestands_Hags.GoogleSheets.Configuration;

[ExcludeFromCodeCoverage]
public class GoogleSheetsSecrets
{
    public string SheetId { get; set; } = null!;
}