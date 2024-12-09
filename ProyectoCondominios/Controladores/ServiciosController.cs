using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using ProyectoCondominios.Models;


namespace ProyectoCondominios.Controladores
{
    public class ServiciosController : Controller
    {
        private static List<Servicio> Servicios = new List<Servicio>
        {
            new Servicio { IdServicio = 3, Nombre = "Agua", Precio = 3500, IdCategoria = 1 , Estado = true },
            new Servicio { IdServicio = 6, Nombre = "Luz", Precio = 17000, IdCategoria = 1, Estado = true },
            new Servicio { IdServicio = 9, Nombre = "Internet", Precio = 25000, IdCategoria = 1, Estado = true },
            new Servicio { IdServicio = 1, Nombre = "Seguridad", Precio = 10000, IdCategoria = 0 , Estado = false }
        };

        public IActionResult Index()
        {
            var serviciosOrdenados = Servicios.OrderBy(s => s.Nombre).ToList();
            return (IActionResult)View(serviciosOrdenados);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return (IActionResult)View();
        }

        [HttpPost]
        public IActionResult Create(Servicio servicio)
        {
            if (ModelState.IsValid)
            {
                servicio.IdServicio = Servicios.Max(s => s.IdServicio) + 1;
                Servicios.Add(servicio);
                return (IActionResult)RedirectToAction("Index");
            }
            return (IActionResult)View(servicio);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var servicio = Servicios.FirstOrDefault(s => s.IdServicio == id);
            if (servicio == null)
            {
                return (IActionResult)HttpNotFound();
            }
            return (IActionResult)View(servicio);
        }

        [HttpPost]
        public IActionResult Edit(Servicio servicio)
        {
            if (ModelState.IsValid)
            {
                var servicioExistente = Servicios.FirstOrDefault(s => s.IdServicio == servicio.IdServicio);
                if (servicioExistente != null)
                {
                    servicioExistente.Nombre = servicio.Nombre;
                    servicioExistente.Precio = servicio.Precio;
                    servicioExistente.IdCategoria = servicio.IdCategoria;
                    servicioExistente.Estado = servicio.Estado;
                    return (IActionResult)RedirectToAction("Index");
                }
            }
            return (IActionResult)View(servicio);
        }
    }
}
