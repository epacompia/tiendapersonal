using Microsoft.AspNetCore.Mvc.Rendering;

namespace tiendapersonal.Models
{
    public class CuentaCreacionViewModel:Cuenta
    {
        public IEnumerable<SelectListItem> TiposCuentas { get; set; }
    }
}
