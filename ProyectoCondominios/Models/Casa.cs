using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoCondominios.Models
{
    public class Casa
    {
        //datos de la casa
        public int IdCasa { get; set; }
        public string NombreCasa { get; set; }
        public decimal MetrosCuadrados { get; set; }
        public int NumeroHabitaciones { get; set; }
        public int NumeroBanos { get; set; }
        public int IdPersona { get; set; }
        public DateTime FechaConstruccion { get; set; }
        public bool Estado { get; set; } // true = Activa, false = Inactiva

    }
}