using System.ComponentModel.DataAnnotations;

namespace Bisopi___Proyectos.ViewModels
{
    public class UserViewModel
    {
        [Display(Name = "ID")]
        public string UserId { get; set; }
        [Display(Name = "Nombre")]
        public string FirstName { get; set; }
        [Display(Name = "Apellido")]
        public string LastName { get; set; }
        [Display(Name = "Usuario")]
        public string UserName { get; set; }
        [Display(Name = "Correo Electronico")]
        public string Email { get; set; }
        [Display(Name = "Ciudad")]
        public string? City { get; set; }
        [Display(Name = "Telefono")]
        public string? Phone { get; set; }
        [Display(Name = "Grupo")]
        public IEnumerable<string> Groups { get; set; }
        [Display(Name = "Cargo")]
        public IEnumerable<string> Roles { get; set; }
    }
}
