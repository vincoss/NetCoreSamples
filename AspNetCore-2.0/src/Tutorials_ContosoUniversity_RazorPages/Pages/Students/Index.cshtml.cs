﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Tutorials_ContosoUniversity_RazorPages.Models;

namespace Tutorials_ContosoUniversity_RazorPages.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly Tutorials_ContosoUniversity_RazorPages.Models.SchoolContext _context;

        public IndexModel(Tutorials_ContosoUniversity_RazorPages.Models.SchoolContext context)
        {
            _context = context;
        }

        public IList<Student> Student { get;set; }

        public async Task OnGetAsync()
        {
            Student = await _context.Student.ToListAsync();
        }
    }
}
