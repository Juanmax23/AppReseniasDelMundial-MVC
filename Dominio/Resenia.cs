using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Resenia : IValidable, IComparable<Resenia>
    {

        private DateTime _fecha;
        private Partido _partido;
        private string _titulo;
        private string _contenido;

        public Resenia(DateTime fecha, Partido partido, string titulo, string contenido)
        {
            this._fecha = fecha;
            this._partido = partido;
            this._titulo = titulo;
            this._contenido = contenido;
        }


        public void Validar()
        {
            ValidarTituloYContenido();
            ValidarPartido();
        }

        public void ValidarTituloYContenido()
        {
            if (string.IsNullOrEmpty(_titulo) && string.IsNullOrEmpty(_contenido)) throw new Exception("Todos los datos son necesarios para la Resenia");
            if (string.IsNullOrEmpty(_titulo)) throw new Exception("El titulo no puede ir vacio");
            if (string.IsNullOrEmpty(_contenido)) throw new Exception("El titulo no puede ir vacio");

        }

        public void ValidarPartido()
        {
            if (_partido == null) throw new Exception("Se necesita un partido para Reseniar sobre el ");
        }

        public int CompareTo(Resenia other)
        {
            return this._fecha.CompareTo(other.Fecha) *-1;
        }

    

        public string Título
        {
            get { return _titulo; }
        }

        public string Contenido
        {
            get { return _contenido; }
        }

        public Partido Partido
        {
            get { return _partido; }
        }

        public DateTime Fecha
        {
            get { return _fecha; }
        }






    }
}
