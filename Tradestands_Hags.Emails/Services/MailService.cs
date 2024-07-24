using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MailKit.Search;
using Microsoft.Extensions.Options;
using MimeKit;
using Tradestands_Hags.Emails.Configuration;
using Tradestands_Hags.Emails.Models;

namespace Tradestands_Hags.Emails.Services;

public class MailService(IOptions<MailSettings> mailSettings) : IMailService
{
    private readonly MailSettings _mailSettings = mailSettings.Value;

    public bool SendMail(MailData mailData)
    {
        try
        {
            using var emailMessage = new MimeMessage();
            var emailFrom =
                new MailboxAddress(_mailSettings.SenderName, _mailSettings.EmailAddress);
            emailMessage.From.Add(emailFrom);
            var emailTo = new MailboxAddress(mailData.EmailToName, mailData.EmailToId);
            emailMessage.To.Add(emailTo);
            emailMessage.Subject = mailData.EmailSubject;

            var emailBodyBuilder = new BodyBuilder
            {
                TextBody = mailData.EmailBody
            };

            emailMessage.Body = emailBodyBuilder.ToMessageBody();

            using var mailClient = new SmtpClient();
            mailClient.Connect(_mailSettings.SmtpServer, _mailSettings.SmtpPort);
            mailClient.Authenticate(_mailSettings.EmailAddress, _mailSettings.Password);
            mailClient.Send(emailMessage);
            AddToSent(emailMessage);
            mailClient.Disconnect(true);

            return true;
        }
        catch (Exception ex)
        {
            // Exception Details
            return false;
        }
    }

    public void GetEmails()
    {
        using var imap = new ImapClient();
        imap.Connect(_mailSettings.ImapServer, _mailSettings.ImapPort);
        imap.Authenticate(_mailSettings.EmailAddress, _mailSettings.Password);

        var inbox = imap.Inbox;
        inbox.Open(FolderAccess.ReadOnly);
        var query = SearchQuery.All;
        var uids = inbox.Search(query);
        var items = inbox.Fetch(uids, MessageSummaryItems.Full | MessageSummaryItems.BodyStructure)
            .Reverse();
        foreach (var messageSummary in items) Console.WriteLine(messageSummary.NormalizedSubject);
    }

    private void AddToSent(MimeMessage message)
    {
        using var imap = new ImapClient();
        imap.Connect(_mailSettings.ImapServer, _mailSettings.ImapPort);
        imap.Authenticate(_mailSettings.EmailAddress, _mailSettings.Password);

        IMailFolder sent;

        if (imap.Capabilities.HasFlag(ImapCapabilities.SpecialUse))
        {
            sent = imap.GetFolder(SpecialFolder.Sent);
        }
        else
        {
            var personal = imap.GetFolder(imap.PersonalNamespaces[0]);
            sent = personal.GetSubfolder("Sent Items");
        }

        // Append the message to the Sent Items folder.
        sent.Append(message, MessageFlags.Seen);

        imap.Disconnect(true);
    }
}