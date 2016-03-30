using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using System.Collections.Concurrent;

using federacionHemofiliaWeb.Interfaces;
using federacionHemofiliaWeb.Models;
using federacionHemofiliaWeb.ViewModels;
using Microsoft.AspNet.Identity;

namespace federacionHemofiliaWeb.Controllers
{
    public class DoctorController : Controller
    {
        [FromServices]
        public IPacienteRepository pacientes { get; set; }

        [FromServices]
        public ICitaRepository citas { get; set; }
        private readonly UserManager<ApplicationUser> _userManager;

        public DoctorController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Paciente()
        {
            var id = await _userManager.FindByEmailAsync(User.Identity.Name);
            var date = DateTime.Now;
            var listaDePacientes = await citas.Get(id.Id, date);
            var pacient = new Dictionary<string, Paciente>();
            foreach (var paciente in listaDePacientes)
            {
                pacient.Add(paciente, await pacientes.get(paciente));
            }

            return View(new PacienteMV
            {
                pacientes = pacient
            });
        }
        
        [HttpGet]
        public async Task<Dictionary<DateTime, int>> pacienteGraph(string id)
        {
            return await pacientes.getData(id);
        }
        //"b21ff511-ae6a-4e0b-9300-ed2389ac8cab"
        //"3faedca2-c7db-44e8-8137-c2e3dced473f"
        
        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult Cita()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cita(CitaVM cita)
        {
            var paciente = await _userManager.FindByEmailAsync(cita.Email);
            var doctor = await _userManager.FindByNameAsync(User.Identity.Name);
            
            if(await citas.Create(doctor.Id, paciente.Id, cita.Fecha))
            {
                return RedirectToAction("Registro");
            }
            return View();
        }
    }
}
