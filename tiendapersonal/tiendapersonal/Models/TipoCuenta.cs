﻿using System.ComponentModel.DataAnnotations;

namespace tiendapersonal.Models
{
    public class TipoCuenta
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="El campo {0} es requerido")]
        [StringLength(maximumLength:50,MinimumLength =3, ErrorMessage ="La longitud del campo {0} debe estar entre {2} y {1} caracteres")]
        public string Nombre { get; set; }
        public int UsuarioId { get; set; }
        public int Orden { get; set; }
    }
}
