using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AplicacionNetCore.Models
{
    public class Contacto
    {
        public int ContactoID { get; set; }
        [Display(Name = "Nombre Contacto")]
        [Required (ErrorMessage = "Ingrese nombre del contacto.")] //Campo requerido
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre debe tener de 3 a 50 caracteres.")] //La cantidad máxima de caracteres
        public string Nombre { get; set; }
        [Display(Name = "Teléfono Contacto")]
        [Required(ErrorMessage = "Ingrese su número telefónico.")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "El teléfono debe tener de 8 a 20 caracteres.")]
        public string Telefono { get; set; }
        [Display(Name = "Dirección Contacto")]
        [StringLength(250, ErrorMessage = "La dirección no debe exceder los 250 caracteres.")]
        public string Direccion { get; set; }
        [Display(Name = "Correo Contacto")]
        [Required(ErrorMessage = "Ingrese su correo electrónico")]
        public string Email { get; set; }
        public bool Verificado { get; set; }
    }
}