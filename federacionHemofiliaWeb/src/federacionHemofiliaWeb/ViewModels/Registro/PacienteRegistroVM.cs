using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace federacionHemofiliaWeb.ViewModels.Registro
{
    public class PacienteRegistroVM
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string LastNmes { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } 

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public string Diagnostic { get; set; }

        [Required]
        public float Peso { get; set; } 

        [Required]
        public float Height { get; set; }
    }
}
