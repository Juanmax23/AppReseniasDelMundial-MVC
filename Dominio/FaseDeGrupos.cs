using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class FaseDeGrupos : Partido, IValidable
    {
        private Grupo _grupo;
        private string _resultado;

        public FaseDeGrupos(Seleccion s1, Seleccion s2, DateTime fechaYHora, Grupo grupo) : base(s1, s2, fechaYHora)
        {
            this._grupo = grupo;
        }

        public FaseDeGrupos(Seleccion s1, Seleccion s2, DateTime fechaYHora, Grupo grupo, bool finalizado) : base(s1, s2, fechaYHora, finalizado)
        {
            this._grupo = grupo;
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

                Finalizado = true;
                int golesSeleccion1 = 0;
                int golesSeleccion2 = 0;

                //Recorro y le sumo a los contadores los goles según el País del jugador
                //Se podria hacer el override del equals en pais para no usar ==
                foreach (Incidencia i in Incidencias)
                {
                    if (i.Inc == "Gol")
                    {
                        if (i.Jugador.Pais == Seleccion1.Pais)
                        {
                            golesSeleccion1++;
                        }
                        else if (i.Jugador.Pais == Seleccion2.Pais)
                        {
                            golesSeleccion2++;
                        }
                    }
                }

                if (golesSeleccion1 > golesSeleccion2)
                {
                    _resultado = $"Ganador: {Seleccion1.NombrePais}.";
                }
                else if (golesSeleccion2 > golesSeleccion1)
                {
                    _resultado = $"Ganador: {Seleccion2.NombrePais}.";
                }
                else
                {
                    _resultado = "Empate";
                }

                return _resultado;
            } else
            {
                throw new Exception("El partido ya está finalizado");

            }

        }



        public override string NombreDelGrupo()
        {
            return "Fase De Grupo : Grupo " + _grupo;
        }

    }
}
