using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net.Mail;
using System.Text;

namespace Plumber71.Core.Service.EmailExcelProvider
{
    public class EmailExcelProvider
    {
        public static string GetLastPriceList()
        {
            var mailRepository = new MailRepository("imap.s.ru", 993, true, "9ru", "*#");
            var attachments = mailRepository.GetAttachments();
            foreach (var a in attachments)
            {
                ZipFile.ExtractToDirectory(a, Path.GetDirectoryName(a));
            }

            return "";
        }
    }
}
