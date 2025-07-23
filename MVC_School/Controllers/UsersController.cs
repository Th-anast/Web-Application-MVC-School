using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_School.Models;

namespace MVC_School.Controllers
{
    public class UsersController : Controller
    {
        private readonly SchoolDBContext _context;

        public UsersController(SchoolDBContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }
        public IActionResult CreateStudent(int? id)
        {
            ViewData["Phone_Number"] = id;
            ViewData["Department"] = HomeController.department;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateStudent(int? id, [Bind("RegistrationNumber,Name,Surname,Department,UsersUsername,UsersUsernameNavigation")] Student student)
        {
            ViewData["Phone_Number"] = id;
            student.UsersUsername = student.UsersUsernameNavigation.Username;
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return Redirect("~/Home/Secretary");
            }
            ViewData["Department"] = HomeController.department;
            return View(student);
        }

        public IActionResult CreateProfessor(int? id)
        {
            ViewData["Phone_Number"] = id;
            ViewData["Department"] = HomeController.department;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProfessor(int? id, [Bind("Afm,Name,Surname,Department,UsersUsername,UsersUsernameNavigation")] Professor professor)
        {
            ViewData["Phone_Number"] = id;
            professor.UsersUsername = professor.UsersUsernameNavigation.Username;
            if (ModelState.IsValid)
            {
                _context.Add(professor);
                await _context.SaveChangesAsync();
                return Redirect("~/Home/Secretary");
            }
            ViewData["Department"] = HomeController.department;
            return View(professor);
        }

    }
}
