using System;
using System.Collections.Generic;


namespace WebApplication_MVC_Navigation_V31.Navigation
{
    public class NavigationItemViewModel
    {
        public NavigationItemViewModel()
        {
            RouteValues = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            AllowedRoles = new List<string>();
            Children = new List<NavigationItemViewModel>();
        }

        public int Id { get; set; }
        public string PageName { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Url { get; set; }
        public int SortOrder { get; set; }
        public string ActionName { get; set; }
        public string Controller { get; set; }
        public NavigationFlags Flag { get; set; }

        public IDictionary<string, object> RouteValues { get; private set; }
        public IList<string> AllowedRoles { get; private set; }
        public IList<NavigationItemViewModel> Children { get; private set; }

        public bool HasItems { get { return ItemsCount > 0; } }
        public int ItemsCount { get { return Children.Count; } }

        public string GetValue(bool flag, string value, string other = null)
        {
            return flag ? value : other;
        }

        public string GetAttributeValue(string key, string defaultValue)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));

            if (RouteValues.ContainsKey(key))
            {
                return RouteValues[key].ToString();
            }
            return defaultValue;
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Title))
            {
                return base.ToString();
            }
            return Title;
        }
    }
}
