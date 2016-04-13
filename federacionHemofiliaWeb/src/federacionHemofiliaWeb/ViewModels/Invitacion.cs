using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace federacionHemofiliaWeb.ViewModels
{
    public class Invitacion
    {
        [Required]
        [EmailAddress]
        public string Correo { get; set; }
    }
}
