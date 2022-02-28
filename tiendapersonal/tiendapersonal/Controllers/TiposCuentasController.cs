using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using tiendapersonal.Models;

namespace tiendapersonal.Controllers
{
    public class TiposCuentasController:Controller
    {
     
        //CONSTRUCTOR
        public TiposCuentasController()
        {
            
        }


        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(TipoCuenta tipoCuenta)
        {
            if (!ModelState.IsValid)
            {
                return View(tipoCuenta);
            }
            return View();
        }
    }
}
