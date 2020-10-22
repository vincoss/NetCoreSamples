using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;


namespace WebApplication_MVC_Navigation_V31.Navigation
{
    public class NavigationService : INavigationService
    {
        private static readonly IList<NavigationItemViewModel> _pages = new List<NavigationItemViewModel>();

        public NavigationService()
        {
            this.MapPage(AppResources.Home, "th", "index", "home").WithFlag(NavigationFlags.Home);

            // Admin
            this.MapPage(AppResources.Administration, "th").WithFlag(NavigationFlags.Administration)
                    .MapChild(AppResources.Users, "index", controllerName: "users").WithFlag(NavigationFlags.AdministrationUsers)
                    .MapChild(AppResources.Roles, "index", controllerName: "roles").WithFlag(NavigationFlags.AdministrationRoles);

            this.MapPage(AppResources.About, "th", "about", "home").WithFlag(NavigationFlags.About);
        }

        public int Count
        {
            get { return _pages.Count; }
        }

        public void AddPage(NavigationItemViewModel page)
        {
            if (page == null) throw new ArgumentNullException(nameof(page));

            _pages.Add(page);
        }

        public IEnumerable<NavigationItemViewModel> GetUserAllowedPages(IPrincipal user, int modules = int.MaxValue)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            // TODO:
            return _pages;

            // Filter out for current users
            //var allowedModels = new List<NavigationItemViewModel>();

            //foreach (var model in _pages)
            //{
            //    // Take only enabled navigation modules
            //    if(IsBitSet(modules, (int)model.Flag) == false)
            //    {
            //        continue;
            //    }

            //    // Take only user accessible pages
            //    var hasAccess = model.AllowedRoles.Any(user.IsInRole);
            //    if (hasAccess)
            //    {
            //        allowedModels.Add(model);
            //    }
            //}

            //return allowedModels.OrderBy(s => s.SortOrder).ThenBy(s => s.Id).ToArray();
        }

        private static bool IsBitSet(int modules, int index)
        {
            return (modules & (1 << index)) != 0;
        }
    }
}
