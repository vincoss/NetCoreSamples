using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WevApps_TagHelpers_V3.Models;

namespace WevApps_TagHelpers_V3.Pages
{
    public class FormsTagHelpersPage : PageModel
    {
        public FormsTagHelpersPage()
        {
            Person = Helper.GetPerson(1);
            Todos = Helper.GetTodos().ToList();
            Address = new RegisterAddressViewModel
            {
                Email = "fero@gmail.com",
                Password = "adbd",
                Address = new AddressViewModel
                {
                    AddressLine1 = "7 Cronin"
                }
            };
            SimpleModel = new SimpleViewModel
            {
                Email = "fero@gmail.com"
            };

            CountryModel = new CountryViewModel
            {
                Country = "Australia"
            };

            EnumCountryModel = new CountryEnumViewModel
            {
                EnumCountry = CountryEnum.Germany
            };

            CountryModelGroup = new CountryViewModelGroup
            {
            };

            CountryCodeList = new CountryViewModelIEnumerable();
            CountryCodeList.CountryCodes = new[] { "FR", "DE" };
        }

        public void OnGet()
        {

        }

        public RegisterViewModel Register { get; set; }
        public RegisterAddressViewModel Address { get; set; }
        public Person Person { get; set; }
        public IList<ToDoItem> Todos { get; set; }
        public DescriptionViewModel Description { get; set; }
        public SimpleViewModel SimpleModel { get; set; }
        public CountryViewModel CountryModel { get; set; }
        public CountryEnumViewModel EnumCountryModel { get; set; }
        public CountryViewModelGroup CountryModelGroup { get; set; }
        public CountryViewModelIEnumerable CountryCodeList { get; set; }

        public bool BoolValue { get; set; }
        public string StringValue { get; set; }
        public DateTime DateTimeValue { get; set; }
        public byte ByteValue { get; set; }
        public int IntValue { get; set; }
        public double DoubleValue { get; set; }
    }
}
