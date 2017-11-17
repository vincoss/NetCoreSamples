using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Default_AspNet_WebApplication_IndividualUserAccounts.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
