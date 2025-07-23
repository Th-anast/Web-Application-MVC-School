using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_School.Models;
using System.Linq;

namespace MVC_School.Controllers
{
    public class CourseHasStudentsController : Controller
    {
        private readonly SchoolDBContext _context;
        public static string? department, afm;
        private static bool flag;

        public CourseHasStudentsController(SchoolDBContext context)
        {
            _context = context;
        }

        // --- Start of Professor's Funtions ---
        public async Task<IActionResult> Index(int? id)
        {
            ViewData["afm"] = id;
            var byTitle = from chs in _context.CourseHasStudents
                          join c in _context.Courses on chs.CourseIdCourse equals c.IdCourse
                          where chs.CourseIdCourse.Equals(c.IdCourse) && c.ProfessorsAfm.Equals(id) && !chs.GradeCourseStudent.Equals(null)
                          join st in _context.Students on chs.StudentsRegistrationNumber equals st.RegistrationNumber
                          select new CourseHasStudent
                          {
                              CourseIdCourseNavigation = new Course { CourseTitle = c.CourseTitle, CourseSemester = c.CourseSemester },
                              StudentsRegistrationNumberNavigation = new Student { Name = st.FullName },
                              GradeCourseStudent = chs.GradeCourseStudent
                          };
            return View(await byTitle.ToListAsync());
        }

        public async Task<IActionResult> ungradedCourses(int? id)
        {
            ViewData["afm"] = afm;
            var byTitle = await _context.CourseHasStudents
                .Include(c => c.CourseIdCourseNavigation)
                .Include(c => c.StudentsRegistrationNumberNavigation)
                .OrderBy(c => c.CourseIdCourseNavigation.CourseSemester)
                .Where(m => m.GradeCourseStudent.Equals(null) & m.CourseIdCourseNavigation.ProfessorsAfmNavigation.Department.Equals(department)).ToListAsync();
            return View(byTitle);
        }

        public async Task<IActionResult> insertGrade(int? id)
        {
            ViewData["afm"] = afm;
            if (id == null || _context.CourseHasStudents == null)
            {
                return NotFound();
            }
            var courseHasStudent = await _context.CourseHasStudents.FindAsync(id);
            if (courseHasStudent == null)
            {
                return NotFound();
            }
            var byTitle = from c in _context.Courses
                          join chs in _context.CourseHasStudents on c.IdCourse equals chs.CourseIdCourse
                          where chs.GradeCourseStudent.Equals(null) && chs.Id.Equals(id) && c.ProfessorsAfmNavigation.Department.Equals(department)
                          select new { c.IdCourse, c.CourseTitle, c.CourseSemester, c.ProfessorsAfm };
            var studentDep = from st in _context.Students
                             join chs in _context.CourseHasStudents on st.RegistrationNumber equals chs.StudentsRegistrationNumber
                             where chs.GradeCourseStudent.Equals(null) && chs.Id.Equals(id) && st.RegistrationNumber.Equals(chs.StudentsRegistrationNumber)
                             select new { st.RegistrationNumber, st.FullName};
            ViewData["CourseIdCourse"] = new SelectList(byTitle, "IdCourse", "CourseTitle", courseHasStudent.CourseIdCourse);
            ViewData["StudentsRegistrationNumber"] = new SelectList(studentDep, "RegistrationNumber", "FullName", courseHasStudent.StudentsRegistrationNumber);
            return View(courseHasStudent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> insertGrade(int id, [Bind("Id,CourseIdCourse,StudentsRegistrationNumber,GradeCourseStudent")] CourseHasStudent courseHasStudent)
        {
            ViewData["afm"] = afm;
            if (id != courseHasStudent.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseHasStudent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseHasStudentExists(courseHasStudent.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ungradedCourses));
            }
            ViewData["CourseIdCourse"] = new SelectList(_context.Courses, "IdCourse", "CourseTitle", courseHasStudent.CourseIdCourse);
            ViewData["StudentsRegistrationNumber"] = new SelectList(_context.Students, "RegistrationNumber", "FullName", courseHasStudent.StudentsRegistrationNumber);
            return View(courseHasStudent);
        }
        // --- End of Professor's Funtions ---

        // --- Start of Student's Funtions ---
        [Route("byCourseTitle/{id}/{flag}")]
        public async Task<IActionResult> byCourseTitle(int? id, bool? flag)
        {
            ViewData["Registration_Number"] = id;
            var byTitle = await _context.CourseHasStudents
                .Include(c => c.CourseIdCourseNavigation)
                .Include(c => c.StudentsRegistrationNumberNavigation)
                .Include(c => c.CourseIdCourseNavigation.ProfessorsAfmNavigation)
                .OrderBy(c => c.CourseIdCourseNavigation.CourseTitle)
                .Where(m => m.StudentsRegistrationNumber.Equals(id) && m.GradeCourseStudent!=null).ToListAsync();
            if (flag.Equals(true))
            {
                byTitle = await _context.CourseHasStudents
                .Include(c => c.CourseIdCourseNavigation)
                .Include(c => c.StudentsRegistrationNumberNavigation)
                .Include(c => c.CourseIdCourseNavigation.ProfessorsAfmNavigation)
                .OrderBy(c => c.CourseIdCourseNavigation.CourseTitle)
                .Where(m => m.StudentsRegistrationNumber.Equals(id)).ToListAsync();
                return View(byTitle);
            }
            return View(byTitle);
        }

        public async Task<IActionResult> byCourseSemester(int? id)
        {
            ViewData["Registration_Number"] = id;
            var bySemister = await _context.CourseHasStudents
                .Include(c => c.CourseIdCourseNavigation)
                .Include(c => c.StudentsRegistrationNumberNavigation)
                .Include(c => c.CourseIdCourseNavigation.ProfessorsAfmNavigation)
                .OrderBy(c => c.CourseIdCourseNavigation.CourseSemester)
                .Where(m => m.StudentsRegistrationNumber.Equals(id)).ToListAsync();            
            return View(bySemister);
        }
        // --- End of Student's Funtions ---

        public async Task<IActionResult> courseDeclaration(int? id)
        {
            ViewData["Phone_Number"] = id;
            ViewData["id"] = _context.CourseHasStudents.Count() + 1;
            var course = await _context.Courses.OrderBy(c => c.CourseTitle).Where(c => c.ProfessorsAfmNavigation.Department.Equals(HomeController.department)).ToListAsync();
            ViewData["IdCourse"] = new SelectList(course, "IdCourse", "CourseTitle");
            var student = await _context.Students.Where(d => d.Department.Equals(HomeController.department)).Distinct().ToListAsync();
            ViewData["RegistrationNumber"] = new SelectList(student, "RegistrationNumber", "FullName");
            if (flag)
            {
                flag = false;
                ViewBag.error = "Τhe statement has already been registered!!!";
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> courseDeclaration(int? id, [Bind("Id,CourseIdCourse,StudentsRegistrationNumber,CourseSemester")] CourseHasStudent courseHasStudent)
        {
            ViewData["Phone_Number"] = id;            
            var course = await _context.Courses.Where(d => d.ProfessorsAfmNavigation.Department.Equals(HomeController.department)).Select(d => d.CourseTitle).ToListAsync();
            var student = await _context.Students.Where(d => d.Department.Equals(HomeController.department)).Select(d => d.FullName).ToListAsync();
            if (ModelState.IsValid)
            {
                var data = _context.CourseHasStudents.Where(x => x.CourseIdCourse.Equals(courseHasStudent.CourseIdCourse) && 
                                                                 x.StudentsRegistrationNumber.Equals(courseHasStudent.StudentsRegistrationNumber));
                if (data.Count().Equals(0))
                {
                    flag = false;
                    _context.Add(courseHasStudent);
                    await _context.SaveChangesAsync();
                    return Redirect("~/Home/Secretary");
                }
                else
                {
                    flag= true;
                    return RedirectToAction("courseDeclaration");
                }
            }            
            ViewData["id"] = _context.CourseHasStudents.Count() + 1;
            ViewData["IdCourse"] = new SelectList(course, "IdCourse", "CourseTitle", courseHasStudent.CourseIdCourse);
            ViewData["RegistrationNumber"] = new SelectList(student, "RegistrationNumber", "FullName", courseHasStudent.StudentsRegistrationNumberNavigation);
            return View(courseHasStudent);
        }

        private bool CourseHasStudentExists(int id)
        {
            return _context.CourseHasStudents.Any(e => e.Id == id);
        }
    }
    
}
