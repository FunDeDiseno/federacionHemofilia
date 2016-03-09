using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace federacionHemofiliaWeb.ViewModels.Registro
{
    public class DoctorVM
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastNames { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} caracteres", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} caracteres", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "No coincide con la contraseña")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Especialidad { get; set; }
    }
}
