using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Default_AspNet_WebApplication_IndividualUserAccounts.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
