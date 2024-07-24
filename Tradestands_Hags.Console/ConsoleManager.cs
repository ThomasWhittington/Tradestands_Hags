using Spectre.Console;
using Tradestands_Hags.Console.Models.Types;
using Tradestands_Hags.Emails.Services;

namespace Tradestands_Hags.Console;

public class ConsoleManager(IMailService mailService)
{
    public void Run()
    {
        var selectedOption = AnsiConsole.Prompt(new SelectionPrompt<ConsoleMenu>()
            .Title("[teal] Select an option[/]:").AddChoices(Enum.GetValues<ConsoleMenu>()));
        AnsiConsole.MarkupLine($"[lime on black]{selectedOption}[/] selected");

        switch (selectedOption)
        {
            case ConsoleMenu.GetEmails:
                mailService.GetEmails();
                break;
            default:
                throw new ArgumentOutOfRangeException(selectedOption.ToString());
        }
    }
}