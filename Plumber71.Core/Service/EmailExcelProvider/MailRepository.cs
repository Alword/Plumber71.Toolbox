using MailKit.Net.Imap;
using MailKit.Search;
using MailKit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MimeKit;
using System.Net.Mail;
using System.IO;

namespace Plumber71.Core.Service.EmailExcelProvider
{
    public class MailRepository
    {
        private readonly string mailServer, login, password;
        private readonly int port;
        private readonly bool ssl;

        public MailRepository(string mailServer, int port, bool ssl, string login, string password)
        {
            this.mailServer = mailServer;
            this.port = port;
            this.ssl = ssl;
            this.login = login;
            this.password = password;
        }

        public IEnumerable<string> GetAttachments()
        {
            List<string> attachments = new List<string>();
            using (var client = new ImapClient())
            {
                client.Connect(mailServer, port, ssl);

                // Note: since we don't have an OAuth2 token, disable
                // the XOAUTH2 authentication mechanism.
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                client.Authenticate(login, password);

                // The Inbox folder is always available on all IMAP servers...
                var inbox = client.Inbox;

                inbox.Open(FolderAccess.ReadOnly);

                var results = inbox.Take(5).ToArray();

                var fromCompany = results.Where(
                    r => r.From.Mailboxes.Where(d => d.Address == "inform@stmgroup.ru").Any()
                    ).FirstOrDefault();

                if (fromCompany != null)
                {
                    foreach (MimeEntity attachment in fromCompany.Attachments)
                    {
                        var fileName = attachment.ContentDisposition?.FileName ?? attachment.ContentType.Name;
                        fileName = Path.Combine($"{Environment.CurrentDirectory}", "Mail", fileName);
                        attachments.Add(fileName);
                        using (var stream = File.Create(fileName))
                        {
                            if (attachment is MessagePart)
                            {
                                var rfc822 = (MessagePart)attachment;

                                rfc822.Message.WriteTo(stream);
                            }
                            else
                            {
                                var part = (MimePart)attachment;

                                part.Content.DecodeTo(stream);
                            }
                        }
                    }
                }

                client.Disconnect(true);
            }

            return attachments;
        }
    }
}
