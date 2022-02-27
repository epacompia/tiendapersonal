using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using tiendapersonal.Models;

namespace tiendapersonal.Controllers
{
    public class TiposCuentasController:Controller
    {
        private readonly string connectionString;
        //CONSTRUCTOR
        public TiposCuentasController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        public IActionResult Crear()
        {

            using (var connection = new SqlConnection(connectionString))
            {
                var query = connection.Query("SELECT 1").FirstOrDefault();
            }

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
