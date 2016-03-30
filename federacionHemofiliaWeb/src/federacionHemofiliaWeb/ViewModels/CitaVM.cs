using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace federacionHemofiliaWeb.ViewModels
{
    public class CitaVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public DateTime Fecha { get; set; }
    }
}
