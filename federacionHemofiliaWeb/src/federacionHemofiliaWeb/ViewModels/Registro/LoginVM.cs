using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace federacionHemofiliaWeb.ViewModels.Registro
{
    public class LoginVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
       
        [Display(Name = "Recordarme")]
        public bool RememberMe { get; set; }
    }
}
