using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio;
using Microsoft.AspNetCore.Http;

namespace Mundial.Controllers
{
    public class PeriodistaController : Controller
    {
        Sistema sistema = Sistema.Instancia;

        public IActionResult RealizarReseñaPartido()
        {
            if (HttpContext.Session.GetString("rol") != null && HttpContext.Session.GetString("rol") == "Periodista")
            {
                try
                {
                    if (TempData.ContainsKey("Exito"))
                    {
                        ViewBag.Exito = TempData["Exito"];
                    }

                    if (TempData.ContainsKey("Error"))
                    {
                        ViewBag.Error = TempData["Error"];
                    }

                    List<Partido> listadoPartidos = sistema.ListarPartidosFinalizados();
                    if (listadoPartidos == null || listadoPartidos.Count == 0) throw new Exception("No existen partidos finalizados Para Reseñar");
                    ViewBag.RealizarReseñaPartido = listadoPartidos;
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

        public IActionResult ProcesarReseña(int partido, string titulo, string contenido)
        {
            if (HttpContext.Session.GetString("rol") != null && HttpContext.Session.GetString("rol") == "Periodista")
            {
                try
                {
                    if (string.IsNullOrEmpty(titulo) && string.IsNullOrEmpty(contenido)) throw new Exception("Todos Los campos son Requeridos");
                    if (string.IsNullOrEmpty(titulo)) throw new Exception("El titulo de la reseña no puede estar vacío");
                    if (string.IsNullOrEmpty(contenido)) throw new Exception("El contenido de la reseña no puede estar vacío");
                    int idPeriodista = (int)HttpContext.Session.GetInt32("id");
                    Periodista periodista = sistema.GetUsuario(idPeriodista) as Periodista;
                    Partido partidoPorId = sistema.BuscarPartidoPorID(partido);
                    if (partidoPorId == null) throw new Exception("No hay partido Seleccionado");
                    if (partidoPorId.Finalizado == false) throw new Exception("No se puede realizar reseñas a un partido que no esta finalizado");
                    DateTime fecha = DateTime.Now.Date;
                    Resenia nuevaReseña = new Resenia(fecha, partidoPorId, titulo, contenido);
                    sistema.AltaReseña(nuevaReseña, periodista);
                    TempData["Exito"] = "La reseña ha sido cargada correctamente.";

                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                }

                return Redirect("RealizarReseñaPartido");
            }
            else
            {
                return View("NoAutorizado");
            }


        }

        public IActionResult VerReseñas()
        {
            if (HttpContext.Session.GetString("rol") != null && HttpContext.Session.GetString("rol") == "Periodista")
            {
                try
                {
                    int idPeriodista = (int)HttpContext.Session.GetInt32("id");
                    Periodista p = sistema.GetUsuario(idPeriodista) as Periodista;
                    if (p == null) throw new Exception("El periodista no ha sido encontrado");
                    List<Resenia> listado = sistema.ListarReseñasPorPeriodista(p);
                    if (listado.Count == 0 || listado == null) throw new Exception("Aún no ha hecho reseñas.");
                    ViewBag.VerReseñas = listado;

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

