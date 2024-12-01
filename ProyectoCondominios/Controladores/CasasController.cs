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



        // GET: Crear Casa
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Propietarios = GetPropietarios();
            return View();
        }

        // POST: Crear Casa
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Casa casa)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("SP_InsertCasa", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@NombreCasa", casa.NombreCasa);
                    command.Parameters.AddWithValue("@MetrosCuadrados", casa.MetrosCuadrados);
                    command.Parameters.AddWithValue("@NumeroHabitaciones", casa.NumeroHabitaciones);
                    command.Parameters.AddWithValue("@NumeroBanos", casa.NumeroBanos);
                    command.Parameters.AddWithValue("@IdPersona", casa.IdPersona);
                    command.Parameters.AddWithValue("@FechaConstruccion", casa.FechaConstruccion);
                    command.Parameters.AddWithValue("@Estado", true);

                    connection.Open();
                    command.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }

            ViewBag.Propietarios = GetPropietarios();
            return View(casa);
        }

        // GET: Editar Casa
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Casa casa = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Casas WHERE id_casa = @IdCasa AND estado = 1", connection);
                command.Parameters.AddWithValue("@IdCasa", id);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    casa = new Casa
                    {
                    

                        IdCasa = Convert.ToInt32(reader["id_casa"]),
                        NombreCasa = reader["nombre_casa"].ToString(),
                        MetrosCuadrados = Convert.ToDecimal(reader["metros_cuadrados"]),
                        NumeroHabitaciones = Convert.ToInt32(reader["numero_habitaciones"]),
                        NumeroBanos = Convert.ToInt32(reader["numero_banos"]),
                        IdPersona = Convert.ToInt32(reader["propietario"]),
                        FechaConstruccion = Convert.ToDateTime(reader["fecha_construccion"]),
                        Estado = Convert.ToBoolean(reader["estado"])
                    };
                }
            }

            if (casa == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Propietarios = GetPropietarios();
            return View(casa);
        }

        // POST: Editar Casa
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Casa casa)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("SP_UpdateCasa", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@IdCasa", casa.IdCasa);
                    command.Parameters.AddWithValue("@NombreCasa", casa.NombreCasa);
                    command.Parameters.AddWithValue("@MetrosCuadrados", casa.MetrosCuadrados);
                    command.Parameters.AddWithValue("@NumeroHabitaciones", casa.NumeroHabitaciones);
                    command.Parameters.AddWithValue("@NumeroBanos", casa.NumeroBanos);
                    command.Parameters.AddWithValue("@IdPersona", casa.IdPersona);
                    command.Parameters.AddWithValue("@FechaConstruccion", casa.FechaConstruccion);

                    connection.Open();
                    command.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }

            ViewBag.Propietarios = GetPropietarios();
            return View(casa);
        }

        // POST: Inactivar Casa
        [HttpPost]
        public ActionResult Inactivate(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("sp_InactivateCasa", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@IdCasa", id);

                connection.Open();
                command.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        // Método auxiliar para obtener propietarios , hay que verificar si funciona
        private SelectList GetPropietarios()
        {
            List<Persona> propietarios = new List<Persona>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT id_persona, nombre FROM Persona", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    propietarios.Add(new Persona
                    {
                        IdPersona = Convert.ToInt32(reader["id_persona"]),
                        Nombre = reader["nombre"].ToString()
                    });
                }
            }

            return new SelectList(propietarios, "id_persona", "nombre");
        }













    }
}
