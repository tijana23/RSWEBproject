using Feit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feit.ViewModels
{
    public class TeacherViewModel
    {
        public IList<Teacher> Teachers { get; set; }
        public string SearchFirstName { get; set; }
        public string SearchLastName { get; set; }
        public string SearchDegree { get; set; }
        public string SearchAcademicRank { get; set; }

    }
}
