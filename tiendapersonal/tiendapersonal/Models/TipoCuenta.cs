﻿using System.ComponentModel.DataAnnotations;
using tiendapersonal.Validaciones;

namespace tiendapersonal.Models
{
    public class TipoCuenta
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="El campo {0} es requerido")]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }
        public int UsuarioId { get; set; }
        public int Orden { get; set; }

       

    }
}
