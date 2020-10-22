using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication_MVC_Navigation_V31.Navigation
{
    public static class NavigationProviderExtensions
    {
        public static NavigationItemViewModel MapPage(this INavigationService pages, string title, string image)
        {
            return MapPage(pages, title, image, null, null);
        }

        public static NavigationItemViewModel MapPage(this INavigationService pages, string title, string image, string actionName, string controllerName)
        {
            return MapPage(pages, title, image, actionName, controllerName, null);
        }

        public static NavigationItemViewModel MapPage(this INavigationService pages, string title, string image, string actionName, string controllerName, object routes)
        {
            if (pages == null)
            {
                throw new ArgumentNullException(nameof(pages));
            }
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentNullException(nameof(title));
            }

            var model = new NavigationItemViewModel
            {
                Id = pages.Count + 1,
                Title = title,
                PageName = title,
                Image = image,
                ActionName = actionName,
                Controller = controllerName
            };

            model.WithRoutes(routes);
            pages.AddPage(model);
            return model;
        }

        public static NavigationItemViewModel MapChild(this NavigationItemViewModel parent, string title, string image = null, string actionName = null, string controllerName = null, object routes = null)
        {
            if (parent == null) throw new ArgumentNullException(nameof(parent));
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentNullException(nameof(title));

            var model = new NavigationItemViewModel
            {
                Id = parent.Children.Count() + 1,
                Title = title,
                PageName = title,
                Image = image,
                ActionName = actionName,
                Controller = controllerName
            };

            model.WithRoutes(routes);
            parent.Children.Add(model);

            return parent;
        }

        public static NavigationItemViewModel WithRoles(this NavigationItemViewModel page, params string[] roles)
        {
            if (page == null) throw new ArgumentNullException(nameof(page));
            if (roles == null) throw new ArgumentNullException(nameof(roles));

            foreach (var role in roles)
            {
                if (page.AllowedRoles.Any(x => x.Equals(role, StringComparison.CurrentCultureIgnoreCase)))
                {
                    continue;
                }

                page.AllowedRoles.Add(role);
            }
            return page;
        }

        public static NavigationItemViewModel WithRoutes(this NavigationItemViewModel page, object routes)
        {
            if (page == null) throw new ArgumentNullException(nameof(page));

            var dict = ObjectToDictionary(routes);

            foreach (var pair in dict)
            {
                page.RouteValues.Add(pair);
            }

            return page;
        }

        public static NavigationItemViewModel WithFlag(this NavigationItemViewModel page, NavigationFlags flags)
        {
            if (page == null) throw new ArgumentNullException(nameof(page));

            page.Flag = flags;
            return page;
        }

        public static NavigationItemViewModel WithName(this NavigationItemViewModel page, string pageName)
        {
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }
            if (string.IsNullOrEmpty(pageName))
            {
                throw new ArgumentNullException(nameof(pageName));
            }
            page.PageName = pageName;
            return page;
        }

        private static IDictionary<string, object> ObjectToDictionary(object value)
        {
            var dictionary = new Dictionary<string, object>();

            if (value == null)
            {
                return dictionary;
            }

            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(value))
            {
                dictionary.Add(descriptor.Name.Replace('_', '-'), descriptor.GetValue(value));
            }

            return dictionary;
        }

        public static RouteValueDictionary ToRouteDictionary(this IDictionary<string, object> values)
        {
            if (values == null)
            {
                return new RouteValueDictionary();
            }
            return new RouteValueDictionary(values);
        }
    }
}
