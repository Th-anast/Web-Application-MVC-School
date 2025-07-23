using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_School.Models;
using System.Runtime.Intrinsics.Arm;

namespace MVC_School.Controllers
{
    public class CoursesController : Controller
    {
        private readonly SchoolDBContext _context;

        public CoursesController(SchoolDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? id)
        {
            ViewData["Phone_Number"] = id;
            var course = await _context.Courses.Include(c => c.ProfessorsAfmNavigation).Where(c=>c.ProfessorsAfmNavigation.Department.Equals(HomeController.department) | c.ProfessorsAfm.Equals(null)).OrderBy(c => c.CourseTitle).ToListAsync();
            return View(course);
        }

        public IActionResult CreateCourse(int? id)
        {
            ViewData["Phone_Number"] = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCourse(int? id, [Bind("IdCourse,CourseTitle,CourseSemester")] Course course)
        {
            ViewData["Phone_Number"] = id;
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return Redirect("~/Home/Secretary");
            }
            return View(course);
        }

        public async Task<IActionResult> assignCourse(int? id)
        {
            ViewData["Phone_Number"] = id;                        
            var title = await _context.Courses.OrderBy(c => c.CourseTitle).Where(c => c.ProfessorsAfm.Equals(null)).ToListAsync();
            ViewData["IdCourse"] = new SelectList(title, "IdCourse", "CourseTitle");
            ViewData["Department"] = HomeController.department;
            var afm = await _context.Professors.Where(d => d.Department.Equals(HomeController.department)).Distinct().ToListAsync();
            ViewData["ProfessorsAfm"] = new SelectList(afm, "Afm","FullName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> assignCourse(int? id, [Bind("IdCourse,ProfessorsAfm")] Course course)
        {
            ViewData["Phone_Number"] = id;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Entry(course).Property("ProfessorsAfm").IsModified=true;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.IdCourse))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect("~/Home/Secretary");
            }
            var title = _context.Courses.OrderBy(c => c.CourseTitle).Where(c => c.ProfessorsAfm.Equals(null)).ToList();
            ViewData["CourseTitle"] = new SelectList(title, "CourseTitle");
            ViewData["Department"] = HomeController.department;
            var afm = _context.Professors.Where(d => d.Department.Equals(HomeController.department)).Select(d => d.FullName).Distinct();
            ViewData["ProfessorsAfm"] = new SelectList(afm, "Afm", "FullName", course.ProfessorsAfm);
            return View(course);
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.IdCourse == id);
        }
    }
}
