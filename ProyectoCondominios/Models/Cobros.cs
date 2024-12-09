using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoCondominios.Models
{
    public class Cobros
    {
        public int Id_cobro { get; set; }

        public int Id_Casa { get; set; }

        public string Nombre_casa { get; set; }

        public decimal Monto { get; set; }

        public string Nombre { get; set; }

        public decimal Precio_casa { get; set; }

        public int Periodo { get; set; }

        public bool Estado { get; set; } //Pendiente o Pagado

        public bool fecha { get; set; }

        public int Mes { get; set; }

        public int Año { get; set; }
    }
}