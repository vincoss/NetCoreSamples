using Microsoft.AspNetCore.Mvc.Razor;


namespace WebApps_RazorSyntax_V3.Classes
{
    public abstract class CustomRazorPage<TModel> : RazorPage<TModel>
    {
        public string CustomText { get; } = "Gardyloo! - A Scottish warning yelled from a window before dumping a slop bucket on the street below.";
    }
}
