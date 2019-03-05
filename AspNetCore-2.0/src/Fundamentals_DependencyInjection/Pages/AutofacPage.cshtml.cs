using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fundamentals_DependencyInjection.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Fundamentals_DependencyInjection.Pages
{
    public class AutofacPageModel : PageModel
    {
        private readonly ICharacterRepository _repository;

        public AutofacPageModel(ICharacterRepository repository)
        {
            _repository = repository;
        }

        public string Message { get; set; }

        public async Task OnGet()
        {
            this.Message = "OnGet()";

            await _repository.WriteMessage("AutofacPageModel.OnGetAsync created this message.");
        }
    }
}