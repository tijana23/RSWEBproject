using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Feit.Data;
using Feit.Models;
using Feit.ViewModels;
using Microsoft.AspNetCore.Hosting;

namespace Feit.Controllers
{
    public class TeachersController : Controller
    {
        private readonly FeitContext _context;

        public TeachersController(FeitContext context)
        {
            _context = context;
        }

        // GET: Teachers
        public async Task<IActionResult> Index(string SearchFirstName, string SearchLastName, string SearchDegree, string SearchAcademicRank)
        {
            IQueryable<Teacher> teachers = _context.Teacher.AsQueryable();
            if (!string.IsNullOrEmpty(SearchFirstName))
            {
                teachers = teachers.Where(s => s.FirstName.Contains(SearchFirstName));
            }
            if (!string.IsNullOrEmpty(SearchLastName))
            {
                teachers = teachers.Where(s => s.LastName.Contains(SearchLastName));
            }
            if (!string.IsNullOrEmpty(SearchDegree))
            {
                teachers = teachers.Where(s => s.Degree.Contains(SearchDegree));
            }
            if (!string.IsNullOrEmpty(SearchAcademicRank))
            {
                teachers = teachers.Where(s => s.AcademicRank.Contains(SearchAcademicRank));
            }
            var teacherVM = new TeacherViewModel
            {
                Teachers = await teachers.ToListAsync()
            };
            return View(teacherVM);
        }

        // GET: Teachers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teacher
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // GET: Teachers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Degree,AcademicRank,OfficeNumber,HireDate")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
               /* string uniqueFileName = UploadedFile(teacher);*/
                /*teacher.ProfilePicture = uniqueFileName;*/
                _context.Add(teacher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
       /*  private string UploadedFile(Teacher model)
        {
            string uniqueFileName = null;

            if (model.ProfileImage != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.ProfileImage.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProfileImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }*/

        // GET: Teachers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teacher.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Degree,AcademicRank,OfficeNumber,HireDate")] Teacher teacher)
        {
            if (id != teacher.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   /* string uniqueFileName = UploadedFile(teacher);*/
                   /* teacher.ProfilePicture = uniqueFileName;*/
                    _context.Update(teacher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherExists(teacher.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(teacher);
        }

        // GET: Teachers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teacher
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teacher = await _context.Teacher.FindAsync(id);
            _context.Teacher.Remove(teacher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Courses(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teacher.Include(t => t.Courses1).ThenInclude(t => t.SecondTeacher).Include(t => t.Courses2).ThenInclude(t => t.FirstTeacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }
        public async Task<IActionResult> Enrollments(int? id, int courseid, int? year)
        {
            if (id == null)
            {
                return NotFound();
            }

            IQueryable<Enrollment> enrollments = _context.Enrollment.AsQueryable();
            enrollments = _context.Enrollment.Include(t => t.Student);
            IQueryable<int> yearQuery = _context.Enrollment.OrderBy(m => m.Year).Select(m => m.Year).Distinct();
            enrollments = enrollments.Where(x => x.CourseId == courseid);
            if (year != null)
            {
                enrollments = enrollments.Where(x => x.Year == year);
            }
            var teacher = await _context.Teacher
                .FirstOrDefaultAsync(t => t.Id == id);
            ViewData["teacher"] = teacher.FullName;
            ViewData["tid"] = id;

            var course = await _context.Course
                .FirstOrDefaultAsync(t => t.Id == courseid);
            ViewData["id"] = courseid;
            ViewData["course"] = course.Title;

            var searchYearVM = new SearchYear
            {
                Years = new SelectList(await yearQuery.ToListAsync()),
                Enrollments = await enrollments.ToListAsync(),
                CourseId = courseid
            };
            return View(searchYearVM);
        }

        public async Task<IActionResult> EnrollDetails(long? id, int tid, long sid, int cid, int year)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewData["id"] = id;
            ViewData["teacher"] = tid;
            ViewData["student"] = sid;
            ViewData["course"] = cid;
            ViewData["year"] = year;

            var enrollment = await _context.Enrollment
                .Include(e => e.Course)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (enrollment == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Course.Where(p => p.Id == cid), "Id", "Title");
            ViewData["StudentId"] = new SelectList(_context.Student.Where(p => p.Id == sid), "Id", "FullName");
            return View(enrollment);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EnrollDetails(long id, [Bind("Id,CourseId,StudentId,Semester,Year,Grade,SeminalUrl,ProjectUrl,ExamPoints,SeminalPoints,ProjectPoints,AdditionalPoints,FinishDate")] Enrollment enrollment, int tid, int cid, int year)
        {
            if (id != enrollment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enrollment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnrollmentExists(enrollment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Enrollments), new { id = tid, courseid = cid, year = year });
            }
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Title", enrollment.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "FullName", enrollment.StudentId);
            return View(enrollment);
        }

        private bool TeacherExists(int id)
        {
            return _context.Teacher.Any(e => e.Id == id);
        }

        private bool EnrollmentExists(long id)
        {
            return _context.Enrollment.Any(e => e.Id == id);
        }
    }
}
