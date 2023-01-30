using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Incidencia
    {
        private string _incidencia;
        private Jugador _jugador;
        private int _minutoIncidencia;

        public Incidencia(string incidencia, Jugador jugador, int minuto)
        {
            this._incidencia = incidencia;
            this._jugador = jugador;
            this._minutoIncidencia = minuto;

        }

        public string Inc
        {
            get { return _incidencia; }
        }

        public Jugador Jugador
        {
            get { return _jugador; }
        }

        public int Minuto
        {
            get { return _minutoIncidencia; }
        }

   

    }
}
