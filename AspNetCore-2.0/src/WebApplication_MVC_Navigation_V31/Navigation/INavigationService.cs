using System;
using System.Collections.Generic;
using System.Security.Principal;


namespace WebApplication_MVC_Navigation_V31.Navigation
{
    public interface INavigationService
    {
        int Count { get; }
        void AddPage(NavigationItemViewModel page);
        IEnumerable<NavigationItemViewModel> GetUserAllowedPages(IPrincipal user, int modules = int.MaxValue);
    }
}
