using CoreEscuela.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreEscuela.App
{
    public class Reporteador
    {
        Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> _diccionario;
        public Reporteador(Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> dicObsEsc)
        {
            if (dicObsEsc == null)
                throw new ArgumentNullException(nameof(dicObsEsc));
            this._diccionario = dicObsEsc;
        }

        public IEnumerable<Evaluacion> GetListaEvaluaciones()
        {
            IEnumerable<Evaluacion> respuesta = new List<Evaluacion>();
            if (_diccionario.TryGetValue(LlaveDiccionario.Evaluacion, out IEnumerable<ObjetoEscuelaBase> lista))
                respuesta = lista.Cast<Evaluacion>();
            return respuesta;
        }

        public IEnumerable<string> GetListaAsignaturas()
        {
            return GetListaAsignaturas(out var dummy);
        }

        public IEnumerable<string> GetListaAsignaturas(out IEnumerable<Evaluacion> listaEvaluaciones)
        {
            listaEvaluaciones = GetListaEvaluaciones();
            return (from Evaluacion evaluacion in listaEvaluaciones
                    select evaluacion.Asignatura.Nombre).Distinct();
        }

        public Dictionary<string, IEnumerable<Evaluacion>> GetDiccionarioEvaluacionesPorAsignatura()
        {
            var diccionarioRespuesta = new Dictionary<string, IEnumerable<Evaluacion>>();
            var listaAsignaturas = GetListaAsignaturas(out var listaEvaluaciones);
            foreach (var asignatura in listaAsignaturas)
            {
                var evaluacionesAsignaturas = from Evaluacion eval in listaEvaluaciones
                                              where eval.Asignatura.Nombre == asignatura
                                              select eval;
                diccionarioRespuesta.Add(asignatura, evaluacionesAsignaturas);
            }
            return diccionarioRespuesta;
        }

        public Dictionary<string, IEnumerable<Object>> GetPromedioPorAsignatura()
        {
            var respuesta = new Dictionary<string, IEnumerable<Object>>();
            var diccionarioEvaluacionesPorAsignatura = GetDiccionarioEvaluacionesPorAsignatura();
            foreach (var asignaturaConEvaluaciones in diccionarioEvaluacionesPorAsignatura)
            {
                var promediosAlumnos = from evaluacion in asignaturaConEvaluaciones.Value
                                       group evaluacion by new
                                       {
                                           evaluacion.Alumno.UniqueId,
                                           evaluacion.Alumno.Nombre
                                       }
                            into grupoEvaluacionesAlumno
                                       select new AlumnoPromedio
                                       {
                                           alumnoid = grupoEvaluacionesAlumno.Key.UniqueId,
                                           alumnoNombre = grupoEvaluacionesAlumno.Key.Nombre,
                                           promedio = grupoEvaluacionesAlumno.Average(evaluacion => evaluacion.Nota)
                                       };
                respuesta.Add(asignaturaConEvaluaciones.Key, promediosAlumnos);
            }
            return respuesta;
        }

        public Dictionary<string, IEnumerable<Object>> GetTopPromedioPorCadaAsignatura(int top)
        {
            var respuesta = new Dictionary<string, IEnumerable<Object>>();
            var diccionarioEvaluacionesPorAsignatura = GetDiccionarioEvaluacionesPorAsignatura();
            foreach (var asignaturaConEvaluaciones in diccionarioEvaluacionesPorAsignatura)
            {
                var promediosAlumnos = (from evaluacion in asignaturaConEvaluaciones.Value
                                        group evaluacion by new
                                        {
                                            evaluacion.Alumno.UniqueId,
                                            evaluacion.Alumno.Nombre
                                        }
                            into grupoEvaluacionesAlumno
                                        select new AlumnoPromedio
                                        {
                                            alumnoid = grupoEvaluacionesAlumno.Key.UniqueId,
                                            alumnoNombre = grupoEvaluacionesAlumno.Key.Nombre,
                                            promedio = grupoEvaluacionesAlumno.Average(evaluacion => evaluacion.Nota)
                                        }).OrderByDescending(alumno => alumno.promedio).Take(top);
                respuesta.Add(asignaturaConEvaluaciones.Key, promediosAlumnos);
            }
            return respuesta;
        }

        public Dictionary<String, IEnumerable<Object>> GetTopPromedioDeUnaAsignatura(string asignatura, int top)
        {
            var respuesta = new Dictionary<string, IEnumerable<Object>>();
            var topPromedios = GetTopPromedioPorCadaAsignatura(top);
            foreach (var asig in topPromedios)
            {
                if (asig.Key.Equals(asignatura))
                {
                    respuesta.Add(asignatura, asig.Value);
                }
            }
            return respuesta;
        }
    }
}