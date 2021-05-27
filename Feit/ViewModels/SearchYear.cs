using Feit.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feit.ViewModels
{
    public class SearchYear
    {
        public IList<Enrollment> Enrollments { get; set; }
        public SelectList Years { get; set; }
        public int Year { get; set; }
        public int CourseId { get; set; }
    }
}