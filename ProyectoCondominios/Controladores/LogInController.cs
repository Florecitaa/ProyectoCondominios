using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoCondominios.Models;
using System.Data.SqlClient; 
using System.Data;


namespace ProyectoCondominios.Controladores
{
    public class LogInController : Controller
    {

        private readonly string connectionString = "PviProyectoFinalDB";
        // GET: LogIn
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string email, string contrasena)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(contrasena))
            {
                ViewBag.Error = "El espacio esta vacio o es nulo.";
                return View();
            }
            Persona persona = SpValidarUsuario(email, contrasena);
            if (persona != null)
            {
                // Almacenar datos en variables de sesión
                HttpContext.Session.SetInt32("IdPersona", persona.IdPersona);
                HttpContext.Session.SetString("NombreCompleto", $"{persona.Nombre} {persona.Apellido}");
                HttpContext.Session.SetString("TipoPersona", persona.TipoPersona);

                // Redirigir según el tipo de persona
                return RedirectToAction("ConsultarCobros", "Cobros");
            }
            else
            {
                ViewBag.Error = "Credenciales incorrectas/ usuario inactivo.";
                return View();
            }
        }



    }
    }
}