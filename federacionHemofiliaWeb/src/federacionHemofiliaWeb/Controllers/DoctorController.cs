using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using System.Collections.Concurrent;

using federacionHemofiliaWeb.Interfaces;
using federacionHemofiliaWeb.Models;
using federacionHemofiliaWeb.ViewModels;

namespace federacionHemofiliaWeb.Controllers
{
    public class DoctorController : Controller
    {
        [FromServices]
        public IPacienteRepository pacientes { get; set; }

        [HttpGet]
        public async Task<IActionResult> Paciente()
        {
            var listaPacientes = await pacientes.get();

            return View(new PacienteMV {
                pacientes = listaPacientes
            });
        }

        public IActionResult Paciente(string id)
        {
            return View(id);
        }
    }
}
