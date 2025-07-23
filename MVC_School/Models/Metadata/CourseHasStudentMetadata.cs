using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_School.Models.Metadata
{
    public class CourseHasStudentMetadata
    {
        [Display(Name = "Course ID")]
        public int CourseIdCourse { get; set; }

        [Display(Name = "Students Registration Number")]
        public int StudentsRegistrationNumber { get; set; }

        [Display(Name = "Grade")]
        public int GradeCourseStudent { get; set; }
    }
}
