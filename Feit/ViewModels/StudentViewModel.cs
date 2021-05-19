using Feit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feit.ViewModels
{
    public class StudentViewModel
    {
        public IList<Student> Students { get; set; }
        public string SearchStudentId { get; set; }
        public string SearchFirstName { get; set; }
        public string SearchLastName { get; set; }
    }
}
