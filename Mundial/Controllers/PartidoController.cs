using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio;
using Microsoft.AspNetCore.Http;

namespace Mundial.Controllers
{
    public class PartidoController : Controller
    {

        Sistema sistema = Sistema.Instancia;
        public IActionResult ListaPartidosJugados()
        {
            if (HttpContext.Session.GetString("rol") != null && HttpContext.Session.GetString("rol") == "Periodista")
            {
                try
                {
                    List<Partido> listadoPartidos = sistema.ListarPartidosFinalizados();
                    if (listadoPartidos == null || listadoPartidos.Count == 0) throw new Exception("No existen partidos finalizados");
                    ViewBag.ListaPartidosJugados = listadoPartidos;
                }
                catch (Exception ex)
                {

                    ViewBag.Error = ex.Message;
                }
                return View();

            }
            if (HttpContext.Session.GetString("rol") != null && HttpContext.Session.GetString("rol") == "Operador")
            {
                try
                {
                    List<Partido> listadoPartidos = sistema.ListarPartidosFinalizados();
                    List<Partido> partidosActivos= sistema.ListarPartidosActivos();
                    listadoPartidos.AddRange(partidosActivos);
                    if (listadoPartidos == null || listadoPartidos.Count == 0) throw new Exception("No existen partidos registrados");
                    ViewBag.ListaPartidosJugados = listadoPartidos;
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

        public IActionResult VisualizarPartidosJugadosEntreDosFechas()
        {
            if (HttpContext.Session.GetString("rol") != null || HttpContext.Session.GetString("rol") == "Operador")
            {

                return View();
            }
            else
            {
                return View("NoAutorizado");

            }

        }

        public IActionResult BuscarPartidosJugadosEntreDosFechas(DateTime fecha1, DateTime fecha2)
        {
            if (HttpContext.Session.GetString("rol") != null && HttpContext.Session.GetString("rol") == "Operador")
            {
                try
                {
                    //Min value es el valor por defecto que tienen los tipo de daro DateTime
                    if (fecha1 == DateTime.MinValue || fecha2 == DateTime.MinValue) throw new Exception("Ninguna de las dos Fechas pueden ir vacias");
                    List<Partido> listadoPartidos = sistema.ListarPartidosFinalizadosEntreDosFechas(fecha1, fecha2);
                    if (listadoPartidos == null || listadoPartidos.Count == 0) throw new Exception("No existen partidos finalizados entre esas dos fechas");
                    ViewBag.PartidoEntreDosFechas = listadoPartidos;

                }
                catch (Exception ex)
                {

                    ViewBag.Error = ex.Message;
                    ViewBag.Fecha1 = fecha1;
                    ViewBag.Fecha2 = fecha2;
                }

                return View("VisualizarPartidosJugadosEntreDosFechas");

            }
            else
            {
                return View("NoAutorizado");
            }
        }

        public IActionResult PartidosConTarjetaRojaDeUnPeriodita()
        {

            if (HttpContext.Session.GetString("rol") != null || HttpContext.Session.GetString("rol") == "Operador")
            {

                return View();
            }
            else
            {
                return View("NoAutorizado");

            }

        }
        public IActionResult BuscarPartidosConTarjetaRojaQueReseñoElPeriodista(string email)
        {
            if (HttpContext.Session.GetString("rol") != null && HttpContext.Session.GetString("rol") == "Operador")
            {
                try
                {
                    if (string.IsNullOrEmpty(email)) throw new Exception("ingres el gmail del periodista que quiera buscar");

                    List<Partido> listaPartidos = sistema.BuscarPartidosConTarjetaRojaQueReseñoUnPeriodista(email.ToLower());
                    if (listaPartidos == null || listaPartidos.Count == 0) throw new Exception("El periodista buscado no tiene reseñas de un partido donde hubo tarjeta roja");
                    ViewBag.ListaPartidos = listaPartidos;
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                }
                return View("PartidosConTarjetaRojaDeUnPeriodita");

            }
            else
            {
                return View("NoAutorizado");
            }

        }

        public IActionResult FinalizarPartido()
        {
            if (HttpContext.Session.GetString("rol") != null && HttpContext.Session.GetString("rol") == "Operador")
            {
                try
                {
                    List<Partido> partidos = new List<Partido>();
                    partidos = sistema.ListarPartidosActivos();
                    if (partidos == null || partidos.Count == 0) throw new Exception("No existen partidos activos");
                    ViewBag.ListadoPartidosSinFinalizar = partidos;
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


        public IActionResult FinalizarPartidoPorID(int idPartido)
        {

            if (HttpContext.Session.GetString("rol") != null && HttpContext.Session.GetString("rol") == "Operador")
            {
                try
                {
                    if (idPartido > 0)
                    {
                        sistema.FinalizarPartidoPorId(idPartido);
                    }
                }
                catch (Exception ex)
                {

                    ViewBag.Error = ex.Message;
                }

                return RedirectToAction("InfoPartidoFinalizado", new { id = idPartido });

            }
            else
            {
                return View("NoAutorizado");
            }

        }

        public IActionResult InfoPartidoFinalizado(int id)
        {

            if (HttpContext.Session.GetString("rol") != null && HttpContext.Session.GetString("rol") == "Operador")
            {
                Partido p = sistema.BuscarPartidoPorID(id);
                try
                {
                    if (p.Finalizado == false) throw new Exception("El partido de Fase Eliminatoria no puede finalizarse en empate"); 
                }
                catch (Exception ex)
                {

                    ViewBag.Error = ex.Message;
                    
                }

                return View(p);
            }
            else
            {
                return View("NoAutorizado");
            }

        }


    }
}
