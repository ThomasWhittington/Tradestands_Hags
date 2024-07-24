using Tradestands_Hags.Emails.Models;

namespace Tradestands_Hags.Emails.Services;

public interface IMailService
{
    bool SendMail(MailData mailData);
    void GetEmails();
}