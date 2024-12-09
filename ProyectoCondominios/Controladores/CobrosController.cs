using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoCondominios.Controladores
{
    public class CobrosController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var cobrosList = new List<Cobros>();
            var list = new List<SpObtenerCobrosConPersonasResult>();
            using (var db = new PviProyectoFinalDB("MyDatabase"))
            {
                list = db.SpObtenerCobrosConPersonas().ToList();
            }


            return View(list);
        }

        public ActionResult CrearCobro(int? id_cobro)
        {
            var cobro = new Cobros();
            try
            {
                using (var db = new PviProyectoFinalDB("MyDatabase"))
                {
                    cobro = db.SeleccionarCobroPorId(id_cobro).Select(_ => new Cobros
                    {
                        Id_cobro = _.Id_cobro,
                        Nombre = _.Nombre_persona,
                        Nombre_casa = _.Nombre_casa,
                        Monto = _.Monto,
                        Precio_casa = _.Precio_casa,
                        Mes = _.Mes,
                        Año = _.Anno
                    }).FirstOrDefault();
                }
            }
            catch (Exception ex) { }
            {

            }
            return View(cobro);
        }
    }
    public ActionResult Crear(Cobros cobro)
    {
        string resultado = String.Empty;
        try
        {
            using (var db = new PviProyectoFinalDB("MyDatabase"))
            {
                if (cobro.Id_cobro == 0)
                {
                    db.SpInsertarBitacoraCobro(cobro.Nombre, cobro.Nombre_casa,
                     cobro.Mes, cobro.Año, cobro.Id_Casa, cobro.Id_cobro);
                }
                else
                {
                    db.SpUpdateCob(cobro.Nombre, cobro.Nombre_casa,
                     cobro.Mes, cobro.Año, cobro.Id_Casa, cobro.Id_cobro);
                }
                ViewBag.Distritos = db.RetornaDistritosCantonID(1).ToList();
                resultado = "Se ha guardado correctamente";

            }
        }
        catch (Exception ex)
        {
            resultado = "No se ha guardado la informacion";
        }
        return View();
    }


    public ActionResult Eliminar(int? id_cobro)
    {
        using (var db = new PviProyectoFinalDB("MyDatabase"))
        {
            db.SpDeleteCobro(id_cobro);
        }
        return RedirectToAction("Index");

    }
}
    }
}