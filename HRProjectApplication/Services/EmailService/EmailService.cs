using HRProjectDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace HRProjectApplication.Services.EmailService
{
    public class EmailService : IEmailService
    {
        public void SendUserInfoEmail(AppUser user, string managerEmail, string pass)
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp-mail.outlook.com");
                client.Port = 587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;

                System.Net.NetworkCredential credential = new System.Net.NetworkCredential("no-reply.hrproject@outlook.com", "hr123boost");
                client.EnableSsl = true;
                client.Credentials = credential;

                MailMessage msg = new MailMessage("no-reply.hrproject@outlook.com", user.Email);
                msg.Subject = "HrApp Account Information";
                string href = $" \" https://grup2hrapp.azurewebsites.net/ \" ";
                msg.Body = String.Format(@"<h2>Hello, {0} {1}, Welcome Abroad!</h2><p>We are thrilled to have you with us! You can find your account information below <br><br>
                            Username : {2}<br> 
                            Password : {3}<br><br>

                            Please sign into your account <a href={4}>here</a> and change your password<br><br>
                            If you have any questions you can contact your manager at {5}<br><br>
                            
                            Best wishes, <br><br>
                            HrApp Team</p>", user.Name, user.Surname, user.UserName, pass, href, managerEmail);

                msg.IsBodyHtml = true;
                client.Send(msg);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SendPasswordResetEmail(AppUser user, string link)
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp-mail.outlook.com");
                client.Port = 587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;

                System.Net.NetworkCredential credential = new System.Net.NetworkCredential("no-reply.hrproject@outlook.com", "hr123boost");
                client.EnableSsl = true;
                client.Credentials = credential;

                MailMessage msg = new MailMessage("no-reply.hrproject@outlook.com", user.Email);
                msg.Subject = "HrApp Account Information";
                string href = link;
                msg.Body = String.Format(@"<h2>Hello, {0} {1}, Please click <a href={2}>here</a> to reset your password</h2><p><br><br>
                            
                            Best wishes, <br><br>
                            HrApp Team</p>", user.Name, user.Surname, href);

                msg.IsBodyHtml = true;
                client.Send(msg);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
