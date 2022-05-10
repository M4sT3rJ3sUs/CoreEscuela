using System;
using System.Collections.Generic;
using System.Reflection;
using CoreEscuela.Entidades;
using CoreEscuela.Util;
using static System.Console;
using System.Linq;

namespace CoreEscuela
{
    class Program
    {
        static void Main(string[] args)
        {
            var engine = new EscuelaEngine();
            engine.Inicializar();
            Printer.WriteTitle("BIENVENIDOS A LA ESCUELA");
            //Printer.Beep(10000, cantidad: 10);
            ImpimirCursosEscuela(engine.Escuela);
            var listaObjetos = engine.GetObjetosEscuela(());


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
    }
}