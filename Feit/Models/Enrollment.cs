using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Feit.Models
{
    public class Enrollment
    {
        public long Id { get; set; }
        [ForeignKey("Course")]
        [Display(Name = "Course ID")]
        public int CourseId { get; set; }
        public Course Course { get; set; }
        [MaxLength(10)]
        public string Semester { get; set; }
        [ForeignKey("Student")]
        [Display(Name = "Student ID")]
        public long StudentId { get; set; }
        public Student Student { get; set; }
        public int Year { get; set; }
        public int Grade { get; set; }
        [MaxLength(255)]
        [Display(Name = "Seminal Url")]

        public string SeminalUrl { get; set; }
        [MaxLength(255)]
        [Display(Name = "Project Url")]
        public string ProjectUrl { get; set; }
        [Display(Name = "Exam Points")]
        public int ExamPoints { get; set; }
        [Display(Name = "Seminal Points")]
        public int SeminalPoints { get; set; }
        [Display(Name = "Project Points")]
        public int ProjectPoints { get; set; }
        [Display(Name = "Additional Points")]
        public int AdditionalPoints { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Finish Date")]
        public DateTime FinishDate { get; set; }
    }
}
