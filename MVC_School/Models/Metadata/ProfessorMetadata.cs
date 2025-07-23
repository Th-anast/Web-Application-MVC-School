using System.ComponentModel.DataAnnotations;

namespace MVC_School.Models.Metadata
{
    public class ProfessorMetadata
    {
        [Display(Name = "AFM")]
        public decimal Afm { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; } = null!;

        [Display(Name = "Surname")]
        public string Surname { get; set; } = null!;

        [Display(Name = "Department")]
        public string Department { get; set; } = null!;

        [Display(Name = "Username")]
        public string UsersUsername { get; set; } = null!;
    }
}
