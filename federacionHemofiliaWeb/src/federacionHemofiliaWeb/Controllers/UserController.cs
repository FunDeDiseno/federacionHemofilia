using Microsoft.AspNet.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Authorization;

using federacionHemofiliaWeb.ViewModels.Registro;
using federacionHemofiliaWeb.Models;
using federacionHemofiliaWeb.Interfaces;

namespace federacionHemofiliaWeb.Controllers
{
    public class UserController : Controller
    {
        [FromServices]
        private IDoctorRepository doctorMethods { get; set; }

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserController(UserManager<ApplicationUser> userManager,
                              SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

         public IActionResult Login()
        {
            return View();
        }   
        // GET: /<controller>/
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro(DoctorVM doctor)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = doctor.Email,
                    Email = doctor.Email
                };

                var result = await _userManager.CreateAsync(user, doctor.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    if (await doctorMethods.Create(new Medico
                    {
                        Citas = new System.Collections.Generic.Dictionary<System.DateTime, string>(),
                        Especialidad = doctor.Especialidad,
                        FirstName=doctor.FirstName,
                        LastNames=doctor.LastNames
                    }))
                    {
                        return RedirectToAction(nameof(HomeController.Index), "Index");
                    }
                }
            }
            return View(doctor);
        }
    }
}