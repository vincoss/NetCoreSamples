﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tutorials_ContosoUniversity_RazorPages.Models
{
    public class CourseViewModel
    {
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        public string DepartmentName { get; set; }
    }
}
