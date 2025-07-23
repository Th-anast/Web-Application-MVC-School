using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MVC_School.Models.Metadata
{
    public class CourseMetadata
    {
        [Display(Name = "Course ID")]
        public int IdCourse { get; set; }

        [Display(Name = "Course Title")]
        public string CourseTitle { get; set; } = null!;

        [Display(Name = "Course Semester")]
        public string CourseSemester { get; set; } = null!;

        [Display(Name = "Professor AFM")]
        public decimal ProfessorsAfm { get; set; }
    }
}
