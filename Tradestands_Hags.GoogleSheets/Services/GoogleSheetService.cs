using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Microsoft.Extensions.Options;
using Tradestands_Hags.GoogleSheets.Configuration;
using static Google.Apis.Sheets.v4.SheetsService;

namespace Tradestands_Hags.GoogleSheets;

public class GoogleSheetService(IOptions<GoogleSheetsSecrets> secrets) : IGoogleSheetService
{
    private readonly GoogleSheetsSecrets _secrets = secrets.Value;
    private SheetsService _sheetsService = null!;

    public async Task Run()
    {
        await ConnectToGoogle();
        const string range = "A1:P";

        SpreadsheetsResource.ValuesResource.GetRequest getRequest =
            _sheetsService.Spreadsheets.Values.Get(_secrets.SheetId, range);

        var getResponse = await getRequest.ExecuteAsync();
        IList<IList<object>> values = getResponse.Values;
        if (values is { Count: > 0 })
        {
            foreach (var row in values)
            {
                for (int i = 0; i < row.Count; i++)
                {
                    Console.Write(row[i] + "\t");
                }

                Console.WriteLine();
            }
        }
    }

    private async Task ConnectToGoogle()
    {
        GoogleCredential credential;
        await using (var stream =
                     new FileStream("google-sheets-credentials.json", FileMode.Open,
                         FileAccess.Read))
        {
            credential = GoogleCredential.FromStream(stream).CreateScoped(Scope.Spreadsheets);
        }

        _sheetsService = new SheetsService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = "Tradestands_Hags"
        });
    }
}