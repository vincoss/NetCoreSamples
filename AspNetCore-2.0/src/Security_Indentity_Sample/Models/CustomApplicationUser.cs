using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Security_Indentity_Sample.Models
{
    // https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity-primary-key-configuration?view=aspnetcore-2.1&tabs=aspnetcore2x

    public class CustomApplicationUser : IdentityUser<Guid>
    {
    }

    public class CustomApplicationRole : IdentityRole<Guid>
    {
    }
}
