using System;

namespace Dominio
{
    public abstract class Usuario : IValidable
    {
        private int _id;
        private static int s_ultId = 1;
        private string _nombre;
        private string _apellido;
        private string _email;
        private string _password;

        public Usuario(string nombre,string apellido, string email, string password)
        {
            this._id = s_ultId;
            s_ultId++;
            this._nombre = nombre;
            this._apellido = apellido;
            this._email = email;
            this._password = password;
        }

        public int Id
        {
            get { return _id; }
        }
        public string Email
        {
            get { return _email; }
        }
        public string Nombre
        {
            get { return _nombre; }
        }

        public string Apellido
        {
            get { return _apellido; }
        }
        public string Password
        {
            get { return _password; }
        }
        public virtual void Validar()
        {
            ValidarNombreYApellido();
            ValidarEmail();
            ValidarContraseña();
        }

        private void ValidarNombreYApellido()
        {
            if (string.IsNullOrEmpty(_nombre)) throw new Exception("El nombre no puede ser vacío");
            if (string.IsNullOrEmpty(_apellido)) throw new Exception("El apellido no puede ser vacío");

        }
        
        private void ValidarEmail()
        {
            if (string.IsNullOrEmpty(_email)) throw new Exception("El email no puede ser vacío");
            if (!(_email.Contains("@"))) throw new Exception("El email debe contener un @");
            if (_email[0] == '@' || _email[_email.Length - 1] == '@') throw new Exception("El @ no puede estar en la primera posicion ni en la ultima");
            ValidarSoloUnArroba();
        }

        public void ValidarSoloUnArroba()
        {

            try
            {
                int count = 0;

                foreach (char g in _email)
                {
                    if (g.Equals('@')) count++;
                }

                if (count != 1) throw new Exception("En el email solo puede haber un @");


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void ValidarContraseña()
        {
            if (_password.Length < 8) throw new Exception("La contraseña tiene que tener por lo menos 8 caracteres");
        }

        public override bool Equals(object obj)
        {
            Usuario p = obj as Usuario;
            return p != null && p.Email.Equals(this._email);
        }

        

        public override string ToString()
        {
            return $"NombreYApellido: {_nombre} Gmail: {_email}";
        }

        public abstract string TipoUsuario();

    }
}
