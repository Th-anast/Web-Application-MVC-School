using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MVC_School.Models;

[Table("course_has_students")]
public partial class CourseHasStudent
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("Course_idCourse")]
    public int? CourseIdCourse { get; set; }

    [Column("Students_RegistrationNumber")]
    public int? StudentsRegistrationNumber { get; set; }

    public int? GradeCourseStudent { get; set; }

    [ForeignKey("CourseIdCourse")]
    [InverseProperty("CourseHasStudents")]
    public virtual Course? CourseIdCourseNavigation { get; set; }

    [ForeignKey("StudentsRegistrationNumber")]
    [InverseProperty("CourseHasStudents")]
    public virtual Student? StudentsRegistrationNumberNavigation { get; set; }
}
