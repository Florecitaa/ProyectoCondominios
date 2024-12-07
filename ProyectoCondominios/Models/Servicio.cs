﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoCondominios.Models
{
    public class Servicio
    {
        public int IdServicio { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int IdCategoria { get; set; }
        public bool Estado { get; set; }

    }
}