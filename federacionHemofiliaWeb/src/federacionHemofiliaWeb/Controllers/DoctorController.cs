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

            return View(new PacienteMV
            {
                pacientes = listaPacientes
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


        
    }
}
