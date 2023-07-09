using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MailKit.Net.Smtp;

namespace EmailSender.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendEmail(string recipient, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Friday", "testingfriday6661@gmail.com"));
            message.To.Add(new MailboxAddress("", recipient));
            message.Subject = subject;
            message.Body = new TextPart("plain")
            {
                Text = body
            };

            using (var client = new SmtpClient())
            {
                client.Connect("sandbox.smtp.mailtrap.io", 587, false);
                client.Authenticate("5491a7aee6a18f", "fb439a85ffbe28");
                client.Send(message);
                client.Disconnect(true);
            }

            return RedirectToAction("Index");
        }
    }
}
