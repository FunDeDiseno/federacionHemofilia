﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace federacionHemofiliaWeb.Controllers
{
    public class ManageController : Controller
    {
        [HttpGet]
        public IActionResult Paciente()
        {
            return View();
        }
    }
}
