using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Options;
using Project_Work_API.Constants;
using Project_Work_API.Services.Interfaces;
using StackExchange.Redis;
using Project_Work_API.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Project_Work_API.Data.Models.DTO;

namespace Project_Work_API.Services.Classes;

public class EmailService : IEmailService
{
    private readonly SmtpSettings _smtpSettings;
    private readonly AppDBContext _appDBContext;
    private readonly IDatabase _redisDb;

    public EmailService(IOptions<SmtpSettings> smtpSettings, AppDBContext appDBContext)
    {
        _smtpSettings = smtpSettings.Value;
        _appDBContext = appDBContext;

        var options = ConfigurationOptions.Parse("localhost:6379");
        options.Password = "SchoolAPI";
        options.AbortOnConnectFail = false;
        var connection = ConnectionMultiplexer.Connect(options);


        var redis = ConnectionMultiplexer.Connect(options);

        _redisDb = redis.GetDatabase();
    }

    public async Task<bool> SendVerificationCodeAsync(string email)
    {
        var user = await _appDBContext.Users.FirstOrDefaultAsync(u => u.Email == email);

        if (user != null)
        {
            var code = new Random().Next(1000, 10000);

            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Constants", "VerificationCode.html");
            var htmlTemplate = File.ReadAllText(templatePath);

            var htmlBody = htmlTemplate
                .Replace("{{UserName}}", user.Name)
                .Replace("{{VerificationCode}}", code.ToString())
                .Replace("{{Year}}", DateTime.Now.Year.ToString());

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("School", _smtpSettings.Email));
            message.To.Add(new MailboxAddress("User", user.Email));
            message.Subject = "Your Verification Code";

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = htmlBody
            };

            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_smtpSettings.Host, _smtpSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_smtpSettings.Email, _smtpSettings.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }

            _redisDb.StringSet(user.Id, code.ToString());

            return true;
        }
        else
            return false;
    }

    public async Task<bool> VerifyEmail(EmailVerifyDataDTO verifyData)
    {
        var user = await _appDBContext.Users.FirstOrDefaultAsync(u => u.Email == verifyData.Email);

        if (user != null)
        {
            var storedCode = await _redisDb.StringGetAsync(user.Id);

            if (storedCode == verifyData.Code)
            {
                await _redisDb.KeyDeleteAsync(user.Id);
                return true;
            }
        }

        return false;
    }
}