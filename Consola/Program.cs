using System;
using System.Collections.Generic;
using Dominio;

namespace Consola
{
    class Program
    {
        static void Main(string[] args)
        {

            int opcion = int.MinValue;
            do
            {
                MostrarTitulo("Menu de opciones");
                MostrarMenu();
                int.TryParse(Console.ReadLine(), out opcion);

                switch (opcion)
                {
                    case 1:
                        DarDeAltaUnPeriodista();
                        break;
                    case 2:
                        AsignarCategoriaFinanciera();
                        break;
                    case 3:
                        ListarTodosLosPartidosEnLosQueHayaParticipadoUnJugador();
                        break;
                    case 4:
                        ListarJugadoresQueHanSidoExpulsadosAlMenosUnaVez();
                        break;
                    case 5:
                        DadoUnNombreDeSeleccionObtenerElPartidoConMasCantidadDeGolesQueHayaPartipado();
                        break;
                    case 6:
                        ListarTodosLosJugadoresQueHayanConvertidoAlMenosUnaVezEnUnPartido();
                        break;
                    case 7:
                        FinalizarPartidoPorId();
                        break;
                    case 0:
                        MostrarMensaje("Saliendo...");
                        break;
                    default:
                        MostrarError("Debe seleccionar una opcion valida");
                        break;
                }

            } while (opcion != 0);


        }

        static void MostrarMenu()
        {
            MostrarMensaje("Ingrese una opcion");
            MostrarMensaje("1 - Dar de Alta un periodista");
            MostrarMensaje("2 - Asignar el valor de referencia para la categoria financiera de los jugadores");
            MostrarMensaje("3 - Lista todos los partidos en los que haya participado un jugador");
            MostrarMensaje("4 - Lista todos los jugadores que han sido expulsados al menos una vez");
            MostrarMensaje("5 - Dado el nombre de una seleccion obtener el partido con más cantidad de goles");
            MostrarMensaje("6 - Lista todos los jugadores que hayan convertido al menos un gol en un partido");
            MostrarMensaje("7 - Finalizar partido por id");
            MostrarMensaje("0 - Salir");
     
        }


        static void MostrarMensaje(string mensaje)
        {
            Console.WriteLine(mensaje);
        }

        static void MostrarTitulo(string titulo)
        {
            Console.WriteLine();
            Console.WriteLine("-------------------------------------------------------------------------------------");
            Console.WriteLine($"**** {titulo}  ****");
            Console.WriteLine("-------------------------------------------------------------------------------------");
            Console.WriteLine();
        }

        static void MostrarError(string error)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"**** {error}  ****");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }

        static void MostrarExito(string exito)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"**** {exito}  ****");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }

        static string PedirPalabras(string mensaje)
        {
            MostrarMensaje(mensaje);
            return Console.ReadLine();
        }

        static int PedirNumeros(string mensaje)
        {
            int numero;
            bool exito = false;
            do
            {
                MostrarMensaje(mensaje);
                exito = int.TryParse(Console.ReadLine(), out numero);
                if (!exito)
                {
                    MostrarError("Ingrese solo numeros");
                }
            } while (!exito);

            return numero;
        }
      
        static double PedirMonto(string mensaje)
        {
            double numero;
            bool exito = false;
            do
            {
                MostrarMensaje(mensaje);
                exito = double.TryParse(Console.ReadLine(), out numero);
                if (!exito)
                {
                    MostrarError("Ingrese un número positivo");
                }
            } while (!exito);

            return numero;
        }

        static void DarDeAltaUnPeriodista()
        {

            try
            {
                MostrarTitulo("Dar de alta periodista");
                string nombreYApellido = PedirPalabras("Ingrese nombre y apellido del periodista");
                if (string.IsNullOrEmpty(nombreYApellido)) throw new Exception("El nombre y apellido ingresado no puede ir vacio");
                string email = PedirPalabras("Ingrese su email");
                if  (string.IsNullOrEmpty(email) )throw new Exception("El email no puede ir vacio");
                string contraseña = PedirPalabras("Ingrese su contraseña");
                if (contraseña.Length < 8) throw new Exception("La contraseña tiene que tener por lo menos 9 caracteres");
                Usuario p = new Usuario(nombreYApellido, email, contraseña);
                Sistema.Instancia.AltaPeriodista(p);
                MostrarExito("Periodista agregado correctamente");
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        static public void AsignarCategoriaFinanciera()
        {
            try
            {
                MostrarTitulo("Asignar el valor de referencia para la categoría financiera de los jugadores");
                double valor = PedirMonto("Ingrese el valor");
                if (valor <= 0) throw new Exception("Ingrese un valor mayor a 0");
                Sistema.Instancia.CambiarMontoBase(valor);
                MostrarExito("El valor ha sido cambiado.");

            } catch (Exception ex)
            {
                MostrarError(ex.Message);
            }

        }
        
        static public void ListarTodosLosPartidosEnLosQueHayaParticipadoUnJugador()
        {
            try
            {
                MostrarTitulo("Mostrar partidos de un jugador");
                int numero = PedirNumeros("Ingrese el ID del jugador buscado");
                if (numero <= 0) throw new Exception("Ingrese un valor valido");
                Jugador j = Sistema.Instancia.BuscarJugadorPorId(numero);
                if (j == null) throw new Exception("No existe un jugador con ese ID");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(j);
                Console.ForegroundColor = ConsoleColor.White;
                List<Partido> aux = Sistema.Instancia.BuscarPartidosPorJugador(numero);

                if(aux.Count == 0)
                {
                    MostrarError("No hay registros");
                } 

                foreach(Partido p in aux)
                {
                    Console.WriteLine(p);
                }    
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        static public void ListarJugadoresQueHanSidoExpulsadosAlMenosUnaVez()
        {
            MostrarTitulo("Lista de jugadores que han sido expulsados");
            try
            {
                List<Jugador> listado = Sistema.Instancia.ListaDeJugadoresQueHanSidoExpulsadosAlMenosUnaVez();

                if (listado.Count == 0) throw new Exception("No hay jugadores expulsados");

                foreach (Jugador j in listado)
                {
                    Console.WriteLine(j);
                }  

            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        static public void DadoUnNombreDeSeleccionObtenerElPartidoConMasCantidadDeGolesQueHayaPartipado()
        {
            try
            {
                MostrarTitulo("Ingrese una selección y se mostrará el partido en que haya participado que tuvo más goles");
                Seleccion seleccion = Sistema.Instancia.BuscarSeleccionPorNombrePais(PedirPalabras("Ingrese el nombre de la selección"));
                if (seleccion == null) throw new Exception("Selecion ingresada no existe");
                List<Partido> partidosConMasGoles = Sistema.Instancia.PartidoConMasGolesEnQueParticipoUnaSeleccion(seleccion);
                if (partidosConMasGoles.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    MostrarMensaje("No hay registros ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                foreach (Partido p in partidosConMasGoles)
                {
                    Console.WriteLine(p);
                }
            }
            catch (Exception e)
            {
                MostrarError(e.Message);
            }
        }

        static public void ListarTodosLosJugadoresQueHayanConvertidoAlMenosUnaVezEnUnPartido()
        {
            try
            {
                MostrarTitulo("Listar jugadores que hayan convertido al menos un gol");
                int id = PedirNumeros("Ingrese el ID del partido buscado");
                Partido p = Sistema.Instancia.BuscarPartidoPorID(id);
                if (p == null) throw new Exception("No existe un partido con ese ID");
                List<Jugador> conGol = (p.ListarJugadoresConGoles()); 
                if(conGol.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    MostrarMensaje("No hay registros de goles en ese partido");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                foreach (Jugador j in conGol)
                {
                    Console.WriteLine(j);
                }
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        static public void FinalizarPartidoPorId()
        {
            try
            {
                MostrarTitulo("Finalizar partido por id");
                int id = PedirNumeros("Ingrese el ID del partido que desea finalizar");
                Partido p = Sistema.Instancia.BuscarPartidoPorID(id);
                if (p == null) throw new Exception("No existe un partido con ese ID");
                MostrarExito("Partido finalizado con éxito");
                Console.WriteLine(Sistema.Instancia.FinalizarPartidoPorId(id));

            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }


    }
}



