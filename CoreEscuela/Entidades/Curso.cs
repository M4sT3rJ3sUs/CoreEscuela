using CoreEscuela.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreEscuela.Entidades
{
    public class Curso
    {
        public string IdentidicadorUnico { get; private set; }
        public string NombreCurso { get; set; }
        public TiposJornada TipoDeJornada { get; set; }

        public Curso() => IdentidicadorUnico = Guid.NewGuid().ToString();
    }
}