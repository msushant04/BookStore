using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using BookStore.Models;
using Microsoft.Extensions.Options;

namespace BookStore.Services
{
    public class EmailService : IEmailService
    {
        private const string EmailTemplatePath = @"EmailTemplate/{0}.html";
        private readonly SMTPConfigModel _smtpConfig;

        public EmailService(IOptions<SMTPConfigModel> smtpConfig)
        {
            _smtpConfig = smtpConfig.Value;
        }
        public async Task SendTestEmail(UserEmailOptions userEmailOptions)
        {
            userEmailOptions.Subject = UpdatePlaceholders(userEmailOptions.Subject, userEmailOptions.Placeholders);
            userEmailOptions.Body = UpdatePlaceholders(GetEmailTemplate("TestEmail"), userEmailOptions.Placeholders);
            await SendEmail(userEmailOptions);
        }
        private async Task SendEmail(UserEmailOptions userEmailOptions)
        {
            try
            {
                MailMessage mail = new MailMessage()
                {
                    Subject = userEmailOptions.Subject,
                    Body = userEmailOptions.Body,
                    From = new MailAddress(_smtpConfig.SenderAddress),
                    IsBodyHtml = _smtpConfig.IsBodyHTML,
                    BodyEncoding = Encoding.Default
                };
                foreach (var toEmail in userEmailOptions.ToEmails)
                {
                    mail.To.Add(toEmail);
                }
                SmtpClient smtpClient = new SmtpClient()
                {
                    Host = _smtpConfig.host,
                    Port = _smtpConfig.Port,
                    EnableSsl = _smtpConfig.EnableSSL,
                    UseDefaultCredentials = _smtpConfig.UseDefaultCredentials,
                    Credentials = new NetworkCredential(_smtpConfig.UserName, _smtpConfig.Password)
                };
                await smtpClient.SendMailAsync(mail);
            }
            catch (Exception ex)
            {

                string msg = ex.ToString();
            }

        }
        public string GetEmailTemplate(string templateName)
        {
            string body = File.ReadAllText(string.Format(EmailTemplatePath, templateName));
            return body;
        }
        private string UpdatePlaceholders(string text, List<KeyValuePair<string, string>> keyValuePairs)
        {
            if (!string.IsNullOrEmpty(text) && keyValuePairs != null)
            {
                foreach (var placeholders in keyValuePairs)
                {
                    if (text.Contains(placeholders.Key))
                    {
                        text = text.Replace(placeholders.Key, placeholders.Value);
                    }
                }
            }
            return text;
        }
    }
}
