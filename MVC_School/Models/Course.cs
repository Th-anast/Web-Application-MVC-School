using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MVC_School.Models;

[Table("course")]
public partial class Course
{
    [Key]
    [Column("idCourse")]
    public int IdCourse { get; set; }

    [StringLength(60)]
    [Unicode(false)]
    public string? CourseTitle { get; set; }

    [StringLength(25)]
    [Unicode(false)]
    public string? CourseSemester { get; set; }

    [Column("Professors_AFM")]
    public int? ProfessorsAfm { get; set; }

    [InverseProperty("CourseIdCourseNavigation")]
    public virtual ICollection<CourseHasStudent> CourseHasStudents { get; } = new List<CourseHasStudent>();

    [ForeignKey("ProfessorsAfm")]
    [InverseProperty("Courses")]
    public virtual Professor? ProfessorsAfmNavigation { get; set; }
}
