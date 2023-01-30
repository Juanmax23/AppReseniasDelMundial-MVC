using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Seleccion : IValidable, IComparable<Seleccion>
    {
        private Pais _pais;
        private List<Jugador> _jugadoresSeleccion = new List<Jugador>();

        public Seleccion(Pais pais, List<Jugador> jugadoresSeleccion)
        {
            this._pais = pais;
            this._jugadoresSeleccion = jugadoresSeleccion;
        }

        public Pais Pais
        {
            get { return _pais; }
        }

        public string NombrePais
        {
            get { return _pais.Nombre; }
        }

        public List<Jugador> JugadoresSeleccion
        {
            get { return _jugadoresSeleccion; }
            set { _jugadoresSeleccion = value; }
        }

        public void Validar()
        {
            ValidarPais();
            ValidarCantidadJugadores();
        }

        private void ValidarPais()
        {
            if (_pais == null) throw new Exception("El país no puede ser nulo");
        }

        private void ValidarCantidadJugadores()
        {
            if (_jugadoresSeleccion.Count < 11) throw new Exception("La selección debe contener al menos 11 jugadores");
        }

        public override string ToString()
        {
            return $"Selección {Pais.Nombre} \nJugadores: {MostrarJugadoresSeleccion()} \n";
        }

        public string MostrarJugadoresSeleccion()
        {
            string listaJugadores = "";
            foreach (Jugador j in _jugadoresSeleccion)
            {
                listaJugadores += $"{j.Nombre}, ";
            }

            return listaJugadores;
        }

        //te dice si el jugador pertenece a la selección
        public bool BuscarJugadorEnUnaSeleccion(Jugador j)
        {
            bool jugadorEsta = false;

            foreach (Jugador ju in _jugadoresSeleccion)
            {
                if (j.Equals(ju)) jugadorEsta = true;
            }

            return jugadorEsta;
        }

        public int CompareTo(Seleccion obj)
        {
            return this._pais.Nombre.CompareTo(obj._pais.Nombre);
        }


    }
}
