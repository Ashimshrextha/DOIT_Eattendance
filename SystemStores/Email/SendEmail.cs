using System.Net;
using System.Net.Mail;

namespace SystemStores.Email
{
    public class SendEmail
    {
        public void sendEmail(EmailModel model)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("sumitaks318@nec.edu.np");
            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("sumitaks318@nec.edu.np", "Idonknw143");
            smtp.Host = "smtp.gmail.com";
            foreach (var item in model.Email)
            {
                mail.To.Add(item);
            }
            foreach (var item in model.CCEmail)
            {
                mail.CC.Add(item);
            }
            foreach (var item in model.BCCEmail)
            {
                mail.Bcc.Add(item);
            }
            mail.IsBodyHtml = model.IsHtml;
            mail.Body = model.Body;
            smtp.Send(mail);
        }
    }
}
