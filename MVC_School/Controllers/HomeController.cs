using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MVC_School.Models;
using System.Diagnostics;

namespace MVC_School.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SchoolDBContext _context;
        public static string userName,role = "";
        public static int id = 0;
        public static string? department;
        private static bool flag;

        public HomeController(ILogger<HomeController> logger, SchoolDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Student()
        {
            ViewData["username"] = userName;
            ViewData["Registration_Number"] = id;
            ViewData["Role"] = role;
            return View();
        }
        public IActionResult Professor()
        {
            ViewData["username"] = userName;
            ViewData["afm"] = CourseHasStudentsController.afm;
            ViewData["Role"] = role;
            return View();
        }
        public IActionResult Secretary()
        {
            ViewData["username"] = userName;
            ViewData["Phone_Number"] = id;
            return View();
        }

        public IActionResult Login()
        {
            if (flag)
            {
                ViewBag.error = "Wrong username or/and password.Try again!!!";
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            if (ModelState.IsValid)
            {                
                var data = _context.Users.Where(s => s.Username.Equals(username) && s.Password.Equals(password)).ToList();
                if (data.Count() > 0)
                {
                    flag = false;
                    userName = username;
                    role = data.First().Role;
                    if (role.Equals("student"))
                    {                        
                        var stu = _context.Students.Where(x=>x.UsersUsername.Equals(username)).ToList();
                        id = stu.First().RegistrationNumber;
                        CourseHasStudentsController.department = stu.First().Department;
                        return RedirectToAction("Student");
                    }
                    else if (data.First().Role.Equals("professor"))
                    {
                        var pro = _context.Professors.Where(x => x.UsersUsername.Equals(username)).ToList();
                        CourseHasStudentsController.afm = pro.First().Afm.ToString();
                        CourseHasStudentsController.department = pro.First().Department;
                        return RedirectToAction("Professor");
                    }
                    else
                    {
                        var secr = _context.Secretaries.Where(x => x.UsersUsername.Equals(username)).ToList();
                        id = secr.First().PhoneNumber;
                        department = secr.First().Department;
                        return RedirectToAction("Secretary");
                    }
                }
                else
                {
                    flag = true;
                    return RedirectToAction("Login");
                }
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}