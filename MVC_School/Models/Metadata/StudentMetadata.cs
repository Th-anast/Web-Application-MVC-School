using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MVC_School.Models.Metadata
{
    public class StudentMetadata
    {
        [Display(Name = "Registration Number")]
        public int RegistrationNumber { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; } = null!;

        [Display(Name = "Surname")]
        public string Surname { get; set; } = null!;

        [Display(Name = "Username")]
        public string UsersUsername { get; set; } = null!;
    }
}
