using System.ComponentModel.DataAnnotations;


namespace WevApps_TagHelpers_V3.Models
{
    public class DescriptionViewModel
    {
        [MinLength(5)]
        [MaxLength(1024)]
        public string Comment { get; set; }
    }
}
