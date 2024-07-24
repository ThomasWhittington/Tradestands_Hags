using System.ComponentModel;
using Tradestands_Hags.Console.Extensions;

namespace Tradestands_Hags.Console.Models.Types;

[TypeConverter(typeof(EnumDescriptionTypeConverter))]
public enum ConsoleMenu
{
    [Description("Get data from google sheet")]
    GoogleSheet,

    [Description("Get email subjects from inbox")]
    GetEmails
}