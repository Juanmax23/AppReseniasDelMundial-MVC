using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio;
using Microsoft.AspNetCore.Http;


namespace Mundial.Controllers
{
    public class UsuarioController : Controller
    {
        Sistema sistema = Sistema.Instancia;
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public IActionResult ProcesarLogin(string email, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)) throw new Exception("Todos los campos son necesarios");
                Usuario u = sistema.LoginUsuario(email, password);
                if (u == null) throw new Exception("Email o Password incorrectos");
                HttpContext.Session.SetString("usuario", u.Nombre);
                HttpContext.Session.SetString("rol", u.TipoUsuario());
                HttpContext.Session.SetInt32("id", u.Id);
                return RedirectToAction("Index", "Home");

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.EmailLog = email;
                return View("Login");

            }

        }

        public IActionResult Registrarse()
        {
            return View();
        }

        public IActionResult ProcesarAltaPeriodista(string nombre,string apellido, string email, string password, string password2)
        {
            try
            {
                if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)) throw new Exception("Todos los campos son requeridos");
                if (password != password2) throw new Exception("La contraseña de verificación no coincide");
                Usuario p = new Periodista(nombre,apellido, email.ToLower(), password);
                sistema.AltaPeriodista(p);
                ViewBag.Exito = "Se creo correctamente el usuario";
            }
            catch (Exception ex )
            {

                ViewBag.Error = ex.Message;
                ViewBag.nombre = nombre;
                ViewBag.apellido = apellido;
                ViewBag.email = email;
                ViewBag.pass = password;
                ViewBag.pass2 = password2;

            }
            return View("Registrarse");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }


    }
}
