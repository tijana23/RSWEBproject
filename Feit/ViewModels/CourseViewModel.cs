using Feit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feit.ViewModels
{
    public class CourseViewModel
    {
        public IList<Course> Courses { get; set; }
        public string SearchProgramme { get; set; }
        public string SearchTitle { get; set; }
        public string SearchSemester { get; set; }
        public IList<Teacher> Teachers { get; set; }

    }
}
