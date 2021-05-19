using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Feit.Models
{
    public class Course
    {
        public int Id { get; set; }
        public int Credits { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(100)]
        public string Programme { get; set; }
        public int Semester { get; set; }
        [MaxLength(20)]
        [Display(Name = "Education Level")]
        public string EducationLevel { get; set; }
        [ForeignKey("FirstTeacher")]
        [Display(Name = "First Teacher")]
        public int? FirstTeacherId { get; set; }
        public Teacher FirstTeacher { get; set; }
        [ForeignKey("SecondTeacher")]
        [Display(Name = "Second Teacher")]
        public int? SecondTeacherId { get; set; }
        public Teacher SecondTeacher { get; set; }
        public ICollection<Enrollment> Student { get; set; }
    }
}
