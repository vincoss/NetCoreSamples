using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApps_Mvc_DependencyInjection.Services
{
    public interface IDateTime
    {
        DateTime Now { get; }
    }
}
