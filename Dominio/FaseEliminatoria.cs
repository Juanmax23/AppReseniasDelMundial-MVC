using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class FaseEliminatoria : Partido, IValidable
    {
        private bool _huboAlargue;
        private bool _huboPenales;
        private Etapa _etapa;
        private string _resultado;

        public FaseEliminatoria(Seleccion s1, Seleccion s2, DateTime fechaYHora, Etapa etapa, bool huboAlargue, bool huboPenales) : base(s1, s2, fechaYHora)
        {
            this._huboAlargue = huboAlargue;
            this._huboPenales = huboPenales;
            this._etapa = etapa;
        }

        public string Resultado
        {
            get { return _resultado; }
            set { _resultado = value; }
        }

        public override string FinalizarPartido()
        {

            if (Finalizado == false)
            {
                int golesSeleccion1 = 0;
                int golesSeleccion2 = 0;
                bool huboPenales = false;
                bool soloAlargue = false;
                //Recorro y le sumo a los contadores los goles según el País del jugador

                foreach (Incidencia i in Incidencias)
                {
                    if (i.Inc.Equals("Gol"))
                    {
                        if (i.Jugador.Pais == Seleccion1.Pais)
                        {
                            golesSeleccion1++;
                        }
                        else if (i.Jugador.Pais == Seleccion2.Pais)
                        {
                            golesSeleccion2++;
                        }

                        if (i.Minuto == -1)
                        {
                            huboPenales = true;
                        }
                        else if (i.Minuto > 90)
                        {
                            soloAlargue = true;
                        }
                    }
                }


                if (!huboPenales && !soloAlargue)
                {
                    if (golesSeleccion1 > golesSeleccion2)
                    {
                        _resultado = $"Ganador: {Seleccion1.NombrePais}.";
                    }
                    else if (golesSeleccion2 > golesSeleccion1)
                    {
                        _resultado = $"Ganador: {Seleccion2.NombrePais}.";
                    }
                }
                else if (!huboPenales && soloAlargue)
                {
                    if (golesSeleccion1 > golesSeleccion2)
                    {
                        _resultado = $"Ganador: {Seleccion1.NombrePais} en alargue.";
                    }
                    else if (golesSeleccion2 > golesSeleccion1)
                    {
                        _resultado = $"Ganador: {Seleccion2.NombrePais} en alargue.";
                    }
                }
                else
                {
                    if (golesSeleccion1 > golesSeleccion2)
                    {
                        _resultado = $"Empate en tiempo de juego. Ganador: {Seleccion1.NombrePais} en tanda de penales.";
                    }
                    else if (golesSeleccion2 > golesSeleccion1)
                    {
                        _resultado = $"Empate en tiempo de juego. Ganador: {Seleccion2.NombrePais} en tanda de penales.";
                    }
                }
                if (golesSeleccion1 != golesSeleccion2)
                {
                    Finalizado = true;
                    return _resultado;
                }
                else
                {
                    return null;
                }

            }
            else
            {
                throw new Exception("El partido ya está finalizado");
            }


        }

        public override string NombreDelGrupo()
        {
            return "Fase Eliminatoria : " + _etapa;
        }
    }
}
