using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Dominio.Ordenamiento
{
    public class OrdenarJugadorPorValorMercadoYNombre: IComparer<Jugador>
    {
        public int Compare([AllowNull] Jugador x, [AllowNull] Jugador y)
        {
            int comparador = 0;
            comparador = (x.ValorMercado.CompareTo(y.ValorMercado) * -1);
            if (comparador == 0)
            {
                comparador = x.Nombre.CompareTo(y.Nombre);
            }
            return comparador;
        }
    }
}
