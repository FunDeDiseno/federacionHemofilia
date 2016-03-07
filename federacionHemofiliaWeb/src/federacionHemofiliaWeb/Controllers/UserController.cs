using Microsoft.AspNet.Mvc;

namespace federacionHemofiliaWeb.Controllers
{
    public class UserController : Controller
    {
        // GET: /<controller>/
        public IActionResult Registro()
        {
            return View();
        }
    }
}