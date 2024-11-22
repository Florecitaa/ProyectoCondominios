using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoCondominios.Models
{
    public class Persona
    {
        public int IdPersona { get; set; } //ID de usuario
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; } //email
        public string Contrasena { get; set; } //password
        public bool Estado { get; set; } // Indica si está activo o inactivo
        public string TipoPersona { get; set; } // Cliente o Empleado
    }
}