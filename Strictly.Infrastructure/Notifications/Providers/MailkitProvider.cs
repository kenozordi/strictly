using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Strictly.Application.Notifications;
using Strictly.Domain.Constants;
using Strictly.Domain.Models.Notifications;
using Strictly.Infrastructure.Configuration.AppSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Infrastructure.Notifications.Providers
{
    public class MailkitProvider : INotificationProvider
    {
        private readonly EmailSettings _emailSettings;

        public MailkitProvider(IOptions<EmailSettings> options)
        {
            _emailSettings = options.Value;
        }

        public async Task<ServiceResult> SendAsync(NotificationRequest notificationRequest)
        {
            using var smtp = new SmtpClient();

            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_emailSettings.From));
                email.To.Add(MailboxAddress.Parse(notificationRequest.To));
                email.Subject = notificationRequest.Subject;
                email.Body = new TextPart("html") { Text = notificationRequest.Message };

                await smtp.ConnectAsync(
                    _emailSettings.Host,
                    _emailSettings.Port,
                    SecureSocketOptions.StartTls);

                await smtp.AuthenticateAsync(
                    _emailSettings.Username,
                    _emailSettings.Passord);

                var emailResponse = await smtp.SendAsync(email);

                return ResponseHelper.ToSuccess(emailResponse);
            }
            catch (Exception ex)
            {
                return ResponseHelper.ToUnprocessable("Something went wrong, please try again later");
            }
            finally
            {
                await smtp.DisconnectAsync(true);
            }

        }
    }
}
