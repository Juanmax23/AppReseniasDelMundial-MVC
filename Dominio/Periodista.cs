using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;


namespace Dominio
{
    public class Periodista : Usuario, IValidable, IComparable<Periodista>
    {
        private List<Resenia> _reseñas = new List<Resenia>();

        public Periodista(string nombre, string apellido, string email, string password) : base(nombre, apellido, email, password)
        {
           
        }

        public List<Resenia> Resenias
        {
            get { return _reseñas; }
        }


        public override string TipoUsuario()
        {
            return "Periodista";
        }

        public void IngresarReseña(Resenia resenia)
        {
            try
            {
                if (resenia == null) throw new Exception("La reseña no puede ser nula");
                _reseñas.Add(resenia);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int CompareTo(Periodista other)
        {
            int comparador = 0;
            comparador = this.Apellido.CompareTo(other.Apellido) * -1;
            if (comparador == 0)
            {
                comparador = this.Nombre.CompareTo(other.Nombre);
            }
            return comparador;
        }
    }
}
