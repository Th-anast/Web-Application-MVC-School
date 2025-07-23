using Microsoft.AspNetCore.Mvc;
using MVC_School.Models.Metadata;
using System.ComponentModel.DataAnnotations;

namespace MVC_School.Models
{
    [ModelMetadataType(typeof(StudentMetadata))]
    public partial class Student
    {
        [Display(Name = "Student's Full Name")]
        public string FullName
        {
            get
            {
                return Name + " " + Surname;
            }
        }
    }
    [ModelMetadataType(typeof(ProfessorMetadata))]
    public partial class Professor
    {
        [Display(Name = "Professor's Full Name")]
        public string FullName
        {
            get
            {
                return Name + " " + Surname;
            }
        }
    }
    [ModelMetadataType(typeof(CourseHasStudentMetadata))]
    public partial class CourseHasStudent
    {

    }
    [ModelMetadataType(typeof(CourseMetadata))]
    public partial class Course
    {

    }
}
