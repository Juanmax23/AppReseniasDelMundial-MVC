using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Dominio;

namespace Dominio
{
    public class Jugador : IValidable 
    {
        private int _id;
        private string _numeroCamiseta;
        private string _nombreCompleto;
        private DateTime _fechaNacimiento;
        private double _altura;
        private string _pieHabil;
        private double _valorMercado;
        private string _monedaValor;
        private Pais _pais;
        private static double s_montoBaseCatFinancera = 5000000;
        private string _puesto;

        public Jugador(int id, string numeroCamiseta, string nombreCompleto, DateTime fechaNacimiento, double altura, string pieHabil, double valorMercado, string moneda, Pais pais, string puesto)
        {
            this._id = id;
            this._numeroCamiseta = numeroCamiseta;
            this._nombreCompleto = nombreCompleto;
            this._fechaNacimiento = fechaNacimiento;
            this._altura = altura;
            this._pieHabil = pieHabil;
            this._valorMercado = valorMercado;
            this._monedaValor = moneda;
            this._pais = pais;
            this._puesto = puesto;

        }

        #region Properties
       
        public string Nombre
        {
            get { return _nombreCompleto; }
        }
        public string PieHabil
        {
            get { return _pieHabil; }
        }
        public double Altura
        {
            get { return _altura; }
        }
        public string Puesto
        {
            get { return _puesto; }
        }
        public double ValorMercado
        {
            get { return _valorMercado; }
        }

        public int Id
        {
            get { return _id; }
        }

        public Pais Pais
        {
            get { return _pais; }
        }

        public static void CambiarMontoBaseJugador(double valor)
        {
            if (valor <= 0) throw new Exception("tiene que ser un numero positivo");
            s_montoBaseCatFinancera = valor;
        }
        
        public string NumeroCamiseta
        {
            get { return _numeroCamiseta; }
        }
        #endregion

        
        #region Validaciones

        public void Validar() 
        {
            ValidarValorMercado();
            ValidarId();
            ValidarNumeroCamiseta();
            ValidarFechaNacimiento();
            ValidarAltura();
            ValidarPieHabil();
            ValidarPais();
            ValidarPuesto();
        }

        private void ValidarValorMercado()
        {
            if (_valorMercado < 0) throw new Exception($"El valor del mercado debe ser mayor a 0 para el jugador {Nombre}");
        }

        private void ValidarId()
        {
            if (_id < 0) throw new Exception($"El id debe ser mayor a 0 para el jugador {Nombre}");
        }

        private void ValidarNumeroCamiseta()
        {
            if (string.IsNullOrEmpty(_numeroCamiseta)) throw new Exception($"El número de camiseta no puede ser nulo para el jugador {Nombre}");
        }

        private void ValidarFechaNacimiento()
        {
            if (_fechaNacimiento == null) throw new Exception($"La fecha de nacimiento no puede ser nula para el jugador {Nombre}");
        }

        public void ValidarAltura()
        {
            if (_altura < 0) throw new Exception($"Ingrese una altura correcta para el jugador {Nombre}");
        }

        public void ValidarPieHabil()
        {
            if (string.IsNullOrEmpty(_pieHabil)) throw new Exception($"El dato pie hábil no puede estar vacío para el jugador {Nombre}");
        }

        public void ValidarPais()
        {
            if (_pais == null) throw new Exception($"Ingrese un país válido para el jugador {Nombre}");

        }

        public void ValidarPuesto()
        {
            if (string.IsNullOrEmpty(_puesto)) throw new Exception($"Ingrese el puesto del jugador para el jugador {Nombre}");
        }

        #endregion

        public override bool Equals(object obj)
        {
            Jugador j = obj as Jugador;
            return j != null && j.Id.Equals(this._id);
        }

        public override string ToString()
        {
            return $" {_nombreCompleto} - Valor {_monedaValor}: {_valorMercado} - Categoria Financiera: {CalcularCategoriaFinanciera()}";
        }

        public string CalcularCategoriaFinanciera()
        {
            string cat = "";

            if (_valorMercado > s_montoBaseCatFinancera)
            {
                cat += "VIP";
            }
            else
            {
                cat += "Estándar";
            }

            return cat;
        }
     
     

    }
}
