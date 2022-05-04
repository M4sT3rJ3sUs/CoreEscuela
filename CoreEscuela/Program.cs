using System;
using CoreEscuela.Entidades;

namespace CoreEscuela
{
    class Program
    {
        static void Main(string[] args)
        {
            static void Main(string[] args)
            {
                var escuela = new Escuela("Platzi Academy", 2012, TiposEscuela.Primaria,
                            ciudad: "Bogotá", pais: "Colombia"
                            );
                Console.WriteLine(escuela);
            }

        }
    }
}
