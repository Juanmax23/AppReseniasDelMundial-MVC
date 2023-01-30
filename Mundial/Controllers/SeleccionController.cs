using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio;
using Microsoft.AspNetCore.Http;

namespace Mundial.Controllers
{
    public class SeleccionController : Controller
    {
        Sistema sistema = Sistema.Instancia;

        public IActionResult ListarSeleccionesConSusJugadores()
        {
            try
            {
                List<Seleccion> listado = sistema.Selecciones();
                if (listado == null || listado.Count == 0) throw new Exception("No se encontro la lista de selleciones");
                ViewBag.ListadoSeleccion = listado;
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return View();
        }


        public IActionResult SeleccionConMasGoles()
        {

            if (HttpContext.Session.GetString("rol") != null && HttpContext.Session.GetString("rol") == "Operador")
            {
                List<Seleccion> seleccionesGoleadoras = sistema.SeleccionesConMasGoles();
                ViewBag.Estadisticas = seleccionesGoleadoras;
                return View();

            }
            else
            {
                return View("NoAutorizado");
            }
        }

    }
}
