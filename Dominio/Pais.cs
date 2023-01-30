using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Pais : IValidable
    {
        private int s_ultId = 1;
        private int _id;
        private string _nombre;
        private string _codigoAlpha3;

        public Pais(string nombre, string codigoAlpha3)
        {
            this._nombre = nombre;
            this._codigoAlpha3 = codigoAlpha3;
            this._id = s_ultId;
            s_ultId++;
        }

        public string Nombre { get { return _nombre; } }
        public string Codigo { get { return _codigoAlpha3; } }

        public void Validar()
        {
            ValidarNombre();
            ValidarCodigo();
        }

        private void ValidarNombre()
        {
            if (string.IsNullOrEmpty(_nombre)) throw new Exception("El nombre no debe ser vacio");
        }

        private void ValidarCodigo()
        {
            if (_codigoAlpha3.Length != 3) throw new Exception("El codigo debe ser siempre 3 caracteres");
            foreach (char c in _codigoAlpha3)
            {
                if (c == '1' || c == '2' || c == '3' || c == '4' || c == '5' || c == '6' || c == '7' || c == '8' || c == '9') throw new Exception("El código Alpha 3 solo puede estar integrado por letras");
            }
        }


        public override bool Equals(object obj)
        {
            Pais p = obj as Pais;
            return p != null && p.Nombre.Equals(this.Nombre);
        }
        

    }
}
