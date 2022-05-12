using System;
using System.Collections.Generic;
using System.Linq;
using CoreEscuela.App;
using CoreEscuela.Entidades;
using CoreEscuela.Util;
using static System.Console;

namespace CoreEscuela
{
    class Program
    {
        static EscuelaEngine engine = new EscuelaEngine();
        static Reporteador reporteador;
        static void Main(string[] args)
        {
            engine.Inicializar();
            Printer.WriteTitle("BIENVENIDOS A LA ESCUELA");
            reporteador = new Reporteador(engine.GetDiccionarioObjetos());
            MostrarMenu();

        }
        private static void ImpimirCursosEscuela(Escuela escuela)
        {
            Printer.WriteTitle("Cursos de la Escuela");
            if (escuela?.Cursos != null)
            {
                foreach (var curso in escuela.Cursos)
                {
                    WriteLine($"Nombre {curso.Nombre  }, Id  {curso.UniqueId}");
                }
            }
        }
        private static void MostrarMenu()
        {
            try
            {
                Printer.WriteTitle("Reporteador de Escuela");
                Console.WriteLine("1. Mostrar lista de evaluaciones");
                Console.WriteLine("2. Mostrar lista de evaluaciónes por asignatura");
                Console.WriteLine("3. Mostrar lista de asignaturas");
                Console.WriteLine("4. Mostrar lista de promedios por asignatura");
                Console.WriteLine("5. Mostrar top de promedios por asignatura");
                Console.WriteLine("6. Mostrar top de promedios de una asignatura");
                Console.WriteLine("7. Finalizar Programa");
                Console.WriteLine("\nIngrese el número de la opción del reporteador");
                int opcionSeleccionada = Int32.Parse(Console.ReadLine());
                switch (opcionSeleccionada)
                {
                    case 1:
                        Console.Clear();
                        Printer.WriteTitle("Mostrando Evaluacion del 1 al 25");
                        var listaEvaluaciones = reporteador.GetListaEvaluaciones();
                        int conteoLineas = 1;
                        foreach (var objeto in listaEvaluaciones)
                        {
                            Console.WriteLine($"{conteoLineas}) {objeto.ToString()}");
                            PresioneEscOenterParaContinuar(conteoLineas, objeto);
                            conteoLineas++;
                        }
                        conteoLineas = 1;
                        FinalizarPrograma();
                        break;

                    case 2:
                        Console.Clear();
                        Printer.WriteTitle("Mostrando Evaluacion del 1 al 25");
                        conteoLineas = 1;
                        var listaEvaluacionesPorAsignatura = reporteador.GetDiccionarioEvaluacionesPorAsignatura();
                        foreach (var asignatura in listaEvaluacionesPorAsignatura)
                        {
                            Printer.WriteTitle($"Evaluaciones de la Asignatura: {asignatura.Key.ToString()}");
                            foreach (var objeto in asignatura.Value)
                            {
                                Console.WriteLine($"{conteoLineas}) {objeto.ToString()}");
                                PresioneEscOenterParaContinuar(conteoLineas, objeto);
                                conteoLineas++;
                            }
                        }
                        conteoLineas = 1;
                        FinalizarPrograma();
                        break;

                    case 3:
                        Console.Clear();
                        Printer.WriteTitle("Mostrando Asignaturas");
                        conteoLineas = 1;
                        var listaAsignaturas = reporteador.GetListaAsignaturas();
                        foreach (var objeto in listaAsignaturas)
                        {
                            Console.WriteLine($"{conteoLineas}) {objeto.ToString()}");
                            PresioneEscOenterParaContinuar(conteoLineas, objeto);
                            conteoLineas++;
                        }
                        conteoLineas = 1;
                        FinalizarPrograma();
                        break;

                    case 4:
                        Console.Clear();
                        Printer.WriteTitle("Mostrando Promedio del 1 al 25");
                        conteoLineas = 1;
                        var diccionarioPromedioPorAsignatura = reporteador.GetPromedioPorAsignatura();
                        foreach (var asignatura in diccionarioPromedioPorAsignatura)
                        {
                            Printer.WriteTitle($"Promedios de la Asignatura: {asignatura.Key.ToString()}");
                            foreach (var objeto in asignatura.Value)
                            {
                                Console.WriteLine($"{conteoLineas}) {objeto.ToString()}");
                                PresioneEscOenterParaContinuar(conteoLineas, objeto);
                                conteoLineas++;
                            }
                        }
                        conteoLineas = 1;
                        FinalizarPrograma();
                        break;

                    case 5:
                        Console.Clear();
                        conteoLineas = 1;
                        try
                        {
                            int top;
                            do
                            {
                                Console.WriteLine("Ingrese el numero del Top de promedios, el rango aceptado es de 3 a 20");
                                top = Int32.Parse(Console.ReadLine());
                            } while (top < 3 || top > 20);
                            Console.Clear();
                            var diccionarioTopPromedioPorCadaAsignatura = reporteador.GetTopPromedioPorCadaAsignatura(top);
                            foreach (var asignatura in diccionarioTopPromedioPorCadaAsignatura)
                            {
                                Printer.WriteTitle($"Promedios de la Asignatura: {asignatura.Key.ToString()}");
                                foreach (var objeto in asignatura.Value)
                                {
                                    Console.WriteLine($"{conteoLineas}) {objeto.ToString()}");
                                    PresioneEscOenterParaContinuar(conteoLineas, objeto);
                                    conteoLineas++;
                                }
                                conteoLineas = 1;
                                Console.WriteLine("\n");
                            }
                            conteoLineas = 1;
                            FinalizarPrograma();
                        }
                        catch (FormatException)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Printer.WriteTitle("No se ingresó un numero valido");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        finally
                        {
                            FinalizarPrograma();
                        }
                        break;

                    case 6:
                        Console.Clear();
                        conteoLineas = 1;
                        try
                        {
                            int top;
                            int opcionAsignatura;

                            Printer.WriteTitle("Asignaturas");
                            var verAsignaturas = reporteador.GetListaAsignaturas();
                            foreach (var objeto in verAsignaturas)
                            {
                                Console.WriteLine($"{conteoLineas}) {objeto.ToString()}");
                                PresioneEscOenterParaContinuar(conteoLineas, objeto);
                                conteoLineas++;
                            }
                            conteoLineas = conteoLineas - 1;
                            do
                            {
                                Console.WriteLine("Ingrese el numero del Top de promedios, el rango aceptado es de 3 a 20");
                                top = Int32.Parse(Console.ReadLine());
                            } while (top < 3 || top > 20);

                            do
                            {
                                Console.WriteLine("Ingrese un numero de asigantura valida");
                                opcionAsignatura = Int32.Parse(Console.ReadLine());
                            } while (opcionAsignatura < 1 || opcionAsignatura > conteoLineas);

                            string opcionAsignaturaString = verAsignaturas.ElementAt(opcionAsignatura - 1);
                            Console.Clear();
                            var diccionarioTopPromedioDeUnaAsignatura = reporteador.GetTopPromedioDeUnaAsignatura(opcionAsignaturaString, top);
                            foreach (var asignatura in diccionarioTopPromedioDeUnaAsignatura)
                            {
                                Printer.WriteTitle($"Promedios de la Asignatura: {asignatura.Key.ToString()}");
                                foreach (var objeto in asignatura.Value)
                                {
                                    Console.WriteLine($"{conteoLineas}) {objeto.ToString()}");
                                    PresioneEscOenterParaContinuar(conteoLineas, objeto);
                                    conteoLineas++;
                                }
                                conteoLineas = 1;
                                Console.WriteLine("\n");
                            }
                            conteoLineas = 1;
                            FinalizarPrograma();
                        }
                        catch (FormatException)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Printer.WriteTitle("No se ingresó un numero valido");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        finally
                        {
                            FinalizarPrograma();
                        }
                        break;

                    case 7:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Printer.WriteTitle("¿Esta seguro que desea finalizar el programa?");
                        Console.ForegroundColor = ConsoleColor.White;
                        FinalizarPrograma();
                        break;

                    default:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("El número de la opción ingresada no es valida\n");
                        Console.ForegroundColor = ConsoleColor.White;
                        MostrarMenu();
                        break;
                }
            }
            catch (FormatException)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Debe ingresar un numero como opción\n");
                Console.ForegroundColor = ConsoleColor.White;
                MostrarMenu();
            }
        }
        public static void PresioneEscOenterParaContinuar(int conteoLineas, ObjetoEscuelaBase objeto)
        {
            if ((conteoLineas % 25) == 0)
            {
                ConsoleKeyInfo tecla;
                Printer.PresioneENTER();
                tecla = Console.ReadKey();
                while (tecla.Key != ConsoleKey.Enter && tecla.Key != ConsoleKey.Escape)
                {
                    Printer.PresioneENTER();
                    tecla = Console.ReadKey();
                };
                if (tecla.Key == ConsoleKey.Escape)
                {
                    Console.Clear();
                    MostrarMenu();
                }
                else
                {
                    Console.Clear();
                    Printer.WriteTitle($"Mostrando {objeto.GetType().Name} del {conteoLineas + 1} al {conteoLineas + 25}");
                }
            }
        }
        public static void PresioneEscOenterParaContinuar(int conteoLineas, Object objeto)
        {
            if ((conteoLineas % 25) == 0)
            {
                ConsoleKeyInfo tecla;
                Printer.PresioneENTER();
                tecla = Console.ReadKey();
                while (tecla.Key != ConsoleKey.Enter && tecla.Key != ConsoleKey.Escape)
                {
                    Printer.PresioneENTER();
                    tecla = Console.ReadKey();
                };
                if (tecla.Key == ConsoleKey.Escape)
                {
                    Console.Clear();
                    MostrarMenu();
                }
                else
                {
                    Console.Clear();
                    Printer.WriteTitle($"Mostrando {objeto.GetType().Name} del {conteoLineas + 1} al {conteoLineas + 25}");
                }
            }
        }
        public static void FinalizarPrograma()
        {
            ConsoleKeyInfo tecla;
            WriteLine("\nPresione ENTER para volver al menú, presione ESC para finalizar el programa...");
            tecla = Console.ReadKey();
            while (tecla.Key != ConsoleKey.Enter && tecla.Key != ConsoleKey.Escape)
            {
                WriteLine("\nPresione ENTER para volver al menú, presione ESC para finalizar el programa...");
                tecla = Console.ReadKey();
            };
            if (tecla.Key == ConsoleKey.Escape)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Printer.WriteTitle("PROGRAMA FINALIZADO");
                Console.ForegroundColor = ConsoleColor.White;
                Environment.Exit(1);
            }
            Console.Clear();
            MostrarMenu();
        }
    }
}