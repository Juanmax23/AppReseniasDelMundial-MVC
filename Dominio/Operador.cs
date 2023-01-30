using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    class Operador : Usuario, IValidable
    {
        private DateTime fechaIngresoTrabajo;

        public Operador(string nombre, string apellido, string email, string password,DateTime fechaIngresoTrabajo) : base(nombre, apellido, email, password)
        {
            this.fechaIngresoTrabajo = fechaIngresoTrabajo;
        }
        public override string TipoUsuario()
        {
            return "Operador";
        }

        public override void Validar()
        {
            base.Validar();
            ValidarFecha();
        }
        public void ValidarFecha()
        {
            if (fechaIngresoTrabajo == DateTime.MinValue) throw new Exception("La fecha En Que comenzo a trabajar no puede ir vacia");
        }
    }
}
