using System.Threading.Tasks;
using BookStore.Models;

namespace BookStore.Services
{
    public interface IEmailService
    {
        string GetEmailTemplate(string TemplateName);
        Task SendTestEmail(UserEmailOptions userEmailOptions);
        Task SendConfirmationEmail(UserEmailOptions userEmailOptions);
        Task SendForgotPasswordEmail(UserEmailOptions userEmailOptions);
    }
}