using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Trendyol.Services.Interfaces;

namespace Trendyol.Services.Classes;

class EmailVerificationService : IEmailVerificationService
{
    public string EmailVerification(string email)
    {
        MailAddress from = new MailAddress("app944407@gmail.com", "Login Programm");
        MailAddress to = new MailAddress(email, "User");
        MailMessage message = new MailMessage(from, to);
        message.Subject = "Your identification code: ";

        Random random = new Random();
        int sendCode = random.Next(1000, 10000);

        message.Body = sendCode.ToString();
        message.IsBodyHtml = true;

        SmtpClient smtp = new SmtpClient();
        smtp.Host = "smtp.gmail.com";
        smtp.Port = 587;
        smtp.EnableSsl = true;
        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtp.UseDefaultCredentials = false;
        smtp.Credentials = new NetworkCredential(from.Address, "ngvqcmropbcdcwyu");

        smtp.Send(message);
        return sendCode.ToString();
    }
}