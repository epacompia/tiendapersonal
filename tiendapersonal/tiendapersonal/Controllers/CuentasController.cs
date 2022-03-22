using Microsoft.AspNetCore.Mvc;

namespace tiendapersonal.Controllers
{
    public class CuentasController:Controller
    {
        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }
    }
}
