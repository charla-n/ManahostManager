using ManahostManager.Domain.Entity;
using System.Collections.Generic;
using System.Net.Mail;

namespace ManahostManager.Utils.MailingUtils
{
    public class MailUtils
    {
        public static List<System.Net.Mail.Attachment> GetAttachments(List<Document> attachmentList, Home currentHome)
        {
            List<System.Net.Mail.Attachment> attachments = new List<System.Net.Mail.Attachment>();

            foreach (Document cur in attachmentList)
                attachments.Add(new Attachment(DocumentUtils.GetDocumentStream((bool)cur.IsPrivate, (string)cur.Url, currentHome.EncryptionPassword), (string)cur.Title));
            return attachments;
        }
    }
}