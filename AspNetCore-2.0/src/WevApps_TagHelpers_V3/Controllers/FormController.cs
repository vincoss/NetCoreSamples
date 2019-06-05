using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WevApps_TagHelpers_V3.Models
{
    public class FormController : Controller
    {
        [Route("/Form/Register", Name = "RegisterRoute")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(string value)
        {
            return Json(value);
        }

        [Route("/Form/Test", Name = "TestRoute")]
        public string Test()
        {
            return "This is the test page";
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string RegisterInput([Bind(Prefix = "Register")] RegisterViewModel model)
        {
            if(model == null)
            {
                return "Model is null";
            }
            return $"{model.Email}-{model.Password}-{nameof(RegisterInput)}";
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string RegisterAddress([Bind(Prefix = "Address")] RegisterAddressViewModel model)
        {
            if (model == null)
            {
                return "Model is null";
            }
            return $"{model.Email}-{model.Password}-{model.Address.AddressLine1}-{nameof(RegisterAddress)}";
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string Edit([Bind(Prefix = "Person")] int age, int colorIndex)
        {
            ViewData["Index"] = colorIndex;
            var person = Helper.GetPerson(age);
            return $"{person.Age}-{person.Colors}-{age}-{colorIndex}-{nameof(Edit)}";
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string EditTodo(string id)
        {
            return $"{id}-{nameof(EditTodo)}";
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string RegisterTextArea([Bind(Prefix = "Description")] DescriptionViewModel model)
        {
            if (model == null)
            {
                return "Model is null";
            }
            return $"{model.Comment}-{nameof(RegisterTextArea)}";
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string RegisterLabel([Bind(Prefix = "SimpleModel")] SimpleViewModel model)
        {
            if (model == null)
            {
                return "Model is null";
            }
            return $"{model.Email}-{nameof(RegisterLabel)}";
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string RegisterValidation([Bind(Prefix = "Register")] RegisterViewModel model)
        {
            if (model == null)
            {
                return "Model is null";
            }

            return $"{model.Email}-{model.Password}-{nameof(RegisterValidation)}";
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string CreateCountry([Bind(Prefix = "CountryModel")] CountryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var msg = model.Country + " selected";
                return model.Country;
            }

            return "Model validation failed";
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string CreateCountryEnum([Bind(Prefix = "EnumCountryModel")] CountryEnumViewModel model)
        {
            if (ModelState.IsValid)
            {
                var msg = model.EnumCountry + " selected";
                return model.EnumCountry.ToString(); ;
            }

            return "Model validation failed";
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string CreateMultiSelectCountry([Bind(Prefix = "CountryCodeList")] MultiSelectionViewModel model)
        {
            if (model == null)
            {
                return "Model is null";
            }

            return $"{string.Join(",", model.CountryCodes)}-{nameof(CreateMultiSelectCountry)}";
        }
    }
}
