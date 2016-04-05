using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Authorization;

using federacionHemofiliaWeb.Models;
using federacionHemofiliaWeb.ViewModels;
using federacionHemofiliaWeb.Interfaces;
using federacionHemofiliaWeb.ViewModels.Registro;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace federacionHemofiliaWeb.Controllers
{
    public class PacienteController : Controller
    {
        [FromServices]
        public IPacienteRepository pacienteMethods { get; set; }

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _applicationDbContext;

        public PacienteController(UserManager<ApplicationUser> userManager,
                                  SignInManager<ApplicationUser> signInManager,
                                  ApplicationDbContext applicationDbContex)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _applicationDbContext = applicationDbContex;
        }

        [HttpPost]
        public async Task<bool> registerApliacion(AplicacionMV aplicacion)
        {
            var user = await pacienteMethods.get(aplicacion.userId);
            user.Aplicaciones.Add(aplicacion.nuevaAplicacion.fecha, aplicacion.nuevaAplicacion.cantidad);
            if(await pacienteMethods.update(user, aplicacion.userId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginVM user)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var userLogged = await _userManager.FindByEmailAsync(user.Email);
                    return Json(userLogged.Id);
                }
                else if (result.IsLockedOut)
                {
                    return Json("locked");
                }
                else
                {
                    return Json("something went wrong");
                }
            }

            return Json("wrong model");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro(ViewModels.Registro.PacienteRegistroVM paciente)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = paciente.Email,
                    Email = paciente.Email
                };

                var getPass = GeneratePassword();

                var result = await _userManager.CreateAsync(user, getPass);
                if (result.Succeeded)
                {
                    var newPaciente = new Paciente
                    {
                        FechaNac = paciente.BirthDate,
                        Estatura = paciente.Height,
                        Peso = paciente.Peso,
                        Severidad = paciente.Diagnostic,
                        Tipo = paciente.Diagnostic,
                        PrimerNombre = paciente.Name,
                        Apellido = paciente.LastNmes,
                        Aplicaciones = new Dictionary<DateTime, int>(),
                        FotoUrl = "http://www.silverlakerowingclub.com/img/placeholder-user.png"
                    };

                    var succed = await pacienteMethods.create(newPaciente, user.Id);
                    pacienteMethods.sendEmail(paciente.Email, getPass);

                    if (succed)
                    {
                        return RedirectToAction("Paciente", "Doctor");
                    }
                }
            }
            return View(paciente);
        }

        private string GeneratePassword(int genlen = 21, bool usenumbers = true, bool uselowalphabets = true, bool usehighalphabets = true, bool usesymbols = true)
        {

            var upperCase = new char[]
                {
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U',
                'V', 'W', 'X', 'Y', 'Z'
                };

            var lowerCase = new char[]
                {
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u',
                'v', 'w', 'x', 'y', 'z'
                };

            var numerals = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            var symbols = new char[]
                {
                '~', '`', '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '{', '[', '}', ']', '-', '_', '=', '+', ':',
                ';', '|', '/', '?', ',', '<', '.', '>'
                };

            char[] total = (new char[0])
                            .Concat(usehighalphabets ? upperCase : new char[0])
                            .Concat(uselowalphabets ? lowerCase : new char[0])
                            .Concat(usenumbers ? numerals : new char[0])
                            .Concat(usesymbols ? symbols : new char[0])
                            .ToArray();

            var rnd = new Random();

            var chars = Enumerable
                .Repeat<int>(0, genlen)
                .Select(i => total[rnd.Next(total.Length)])
                .ToArray();

            return new string(chars);
        }
    }
}
