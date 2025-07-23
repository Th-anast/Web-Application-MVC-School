using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MVC_School.Models;

[Table("secretaries")]
public partial class Secretary
{
    [Key]
    public int PhoneNumber { get; set; }

    [StringLength(45)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(45)]
    [Unicode(false)]
    public string Surname { get; set; } = null!;

    [StringLength(45)]
    [Unicode(false)]
    public string Department { get; set; } = null!;

    [Column("Users_username")]
    [StringLength(45)]
    [Unicode(false)]
    public string UsersUsername { get; set; } = null!;

    [ForeignKey("UsersUsername")]
    [InverseProperty("Secretaries")]
    public virtual User UsersUsernameNavigation { get; set; } = null!;
}
