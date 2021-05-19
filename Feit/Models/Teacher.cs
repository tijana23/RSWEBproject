using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Feit.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        [MaxLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [MaxLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [MaxLength(50)]
        public string Degree { get; set; }
        [MaxLength(25)]
        [Display(Name = "Academinc Rank")]
        public string AcademicRank { get; set; }
        [MaxLength(10)]
        [Display(Name = "Office Number")]
        public string OfficeNumber { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }
        [InverseProperty("FirstTeacher")]
        public ICollection<Course> Courses1 { get; set; }
        [InverseProperty("SecondTeacher")]
        public ICollection<Course> Courses2 { get; set; }
        public string FullName
        {
            get { return LastName + " " + FirstName; }
        }
    }
    
}
