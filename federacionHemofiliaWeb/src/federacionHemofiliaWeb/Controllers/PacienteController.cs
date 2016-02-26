using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

using federacionHemofiliaWeb.Models;
using federacionHemofiliaWeb.ViewModels;
using federacionHemofiliaWeb.Interfaces;
// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace federacionHemofiliaWeb.Controllers
{
    public class PacienteController : Controller
    {
        [FromServices]
        private IPacienteRepository paciente { get; set; }

        [HttpPost]
        public async Task<bool> registerApliacion(AplicacionMV aplicacion)
        {
            var user = await paciente.get(aplicacion.userId);
            user.Aplicaciones.Add(aplicacion.nuevaAplicacion.fecha, aplicacion.nuevaAplicacion.cantidad);
            if(await paciente.update(user, aplicacion.userId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}
