using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio;
using Microsoft.AspNetCore.Http;

namespace Mundial.Controllers
{
    public class OperadorController : Controller
    {
        Sistema sistema = Sistema.Instancia;
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("rol") != null && HttpContext.Session.GetString("rol") == "Operador")
            {
                return View();

            }
            else
            {
                return View("NoAutorizado");
            }

        }

       

        public IActionResult VerPeriodistas()
        {

            if (HttpContext.Session.GetString("rol") != null && HttpContext.Session.GetString("rol") == "Operador")
            {

                try
                {
                    List<Periodista> listado = sistema.VisualizarPeriodistas();
                    if (listado == null) throw new Exception("La lista de periodistas no puede estar nula");
                    ViewBag.listaPeriodistas = listado;
                }
                catch (Exception ex)
                {

                    ViewBag.Error = ex.Message;
                }

                return View();

            }
            else
            {
                return View("NoAutorizado");
            }


        }

        public IActionResult VisualizarReseñasDeUnPeriodista(string email)
        {
            if (HttpContext.Session.GetString("rol") != null && HttpContext.Session.GetString("rol") == "Operador")
            {
                try
                {
                    Periodista p = sistema.BuscarUsuarioPorEmail(email) as Periodista;
                    if (p == null) throw new Exception("El periodista no ha sido encontrado");
                    List<Resenia> listado = sistema.ListarReseñasPorPeriodista(p);
                    ViewBag.VerReseñas = listado;
                    ViewBag.Nombre = p.Nombre;
                }
                catch (Exception e)
                {

                    ViewBag.Error = e.Message;
                }
                return View();
            }

            else
            {
                return View("NoAutorizado");
            }
        }

    }
}
