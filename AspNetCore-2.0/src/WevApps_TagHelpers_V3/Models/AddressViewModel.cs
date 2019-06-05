using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WevApps_TagHelpers_V3.Models
{
    public class AddressViewModel
    {
        public string AddressLine1 { get; set; }
    }

    public class RegisterAddressViewModel
    {
        public RegisterAddressViewModel()
        {
            Address = new AddressViewModel();
        }

        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public AddressViewModel Address { get; set; }
    }
}
