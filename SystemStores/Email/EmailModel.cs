using System.Collections.Generic;
using System.Net.Mail;

namespace SystemStores.Email
{
    public class EmailModel
    {
        public List<MailAddress> Email { get; set; }
        public List<MailAddress> CCEmail { get; set; }
        public List<MailAddress> BCCEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtml { get; set; }
    }
}
