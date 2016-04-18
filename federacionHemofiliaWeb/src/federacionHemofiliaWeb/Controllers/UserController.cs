using Microsoft.AspNet.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc.Razor;

using federacionHemofiliaWeb.ViewModels.Registro;
using federacionHemofiliaWeb.Models;
using federacionHemofiliaWeb.Interfaces;

namespace federacionHemofiliaWeb.Controllers
{
    public class UserController : Controller
    {
        [FromServices]
        public IDoctorRepository doctorMethods { get; set; }

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _applicationDbContext;


        public UserController(UserManager<ApplicationUser> userManager,
                              SignInManager<ApplicationUser> signInManager,
                              ApplicationDbContext applicationDbContex)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _applicationDbContext = applicationDbContex;
        }

        [HttpGet]
        [AllowAnonymous]
         public IActionResult Login()
        {
            return View();
        }   

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro(DoctorVM doctor, string Id=null)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = doctor.Email,
                    Email = doctor.Email
                };

                if(Id != null)
                {
                    if (Id == doctorMethods.GetHash(doctor.Email))
                    {
                        var result = await _userManager.CreateAsync(user, doctor.Password);
                        if (result.Succeeded)
                        {
                            await _signInManager.SignInAsync(user, isPersistent: false);

                            var newDoctor = new Medico
                            {
                                Especialidad = doctor.Especialidad,
                                FirstName = doctor.FirstName,
                                LastNames = doctor.LastNames,
                                Citas = new System.Collections.Generic.Dictionary<System.DateTime, string>()
                            };

                            var succeded = await doctorMethods.Create(newDoctor, user.Id);

                            if (succeded)
                            {
                                return RedirectToAction("Inicio", "Doctor");
                            }
                        }
                    }
                }
                else
                {
                    var result = await _userManager.CreateAsync(user, doctor.Password);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);

                        var newDoctor = new Medico
                        {
                            Especialidad = doctor.Especialidad,
                            FirstName = doctor.FirstName,
                            LastNames = doctor.LastNames,
                            Citas = new System.Collections.Generic.Dictionary<System.DateTime, string>()
                        };

                        var succeded = await doctorMethods.Create(newDoctor, user.Id);

                        if (succeded)
                        {
                            return RedirectToAction("Inicio", "Doctor");
                        }
                    }

                }
            }
            return View(doctor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM user)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Inicio", "Doctor");
                }
                if (result.IsLockedOut)
                {
                    return View("Se ha bloqueado tu usario...");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Inicio de sesion incorrecto");
                    return View(user);
                }
            }

            return View(user);
        }
    }
}