using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace federacionHemofiliaWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Metodo()
        {
            return View();
        }
        
        public IActionResult MetodoMarco()
        {
            return View();
        }
        
        public IActionResult Acerca()
        {
            return View();
        }
        
        public IActionResult Metodologia()
        {
            return View();
            //return RedirectToAction("Index");
        }
        
        public IActionResult Evil()
        {
            return View();
        }
    }
}
