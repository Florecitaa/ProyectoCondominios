using DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoCondominios.Controladores
{
    //falta verificacion sobre usuario 
    public class CasasController : Controller
    {
        private readonly string connectionString = "PviProyectoFinalDB";

        [HttpGet]
        public ActionResult Index()
        {
            List<Casa> casas = new List<Casa>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SP_GetCasas", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    casas.Add(new Casa
                    {
                        IdCasa = Convert.ToInt32(reader["id_casa"]),
                        NombreCasa = reader["nombre_casa"].ToString(),
                        MetrosCuadrados = Convert.ToDecimal(reader["metros_cuadrados"]),
                        NumeroHabitaciones = Convert.ToInt32(reader["numero_habitaciones"]),
                        NumeroBanos = Convert.ToInt32(reader["numero_banos"]),
                        IdPersona = Convert.ToInt32(reader["propietario"]),
                        FechaConstruccion = Convert.ToDateTime(reader["fecha_construccion"]),
                        Estado = Convert.ToBoolean(reader["estado"])
                    });
                }
            }

            return View(casas);
        }

















    }
}
