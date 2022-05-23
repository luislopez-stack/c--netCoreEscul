using System.Collections.Generic;
using NetCoreEscu.Entidades;
using System;
using System.Linq;

namespace NetCoreEscu
{
    public class Reporteador
    {
        //Campos
        private Dictionary<LlavesDiccionario, IEnumerable<ObjetoEscuelaBase>> _diccionario;

        public Reporteador(Dictionary<LlavesDiccionario, IEnumerable<ObjetoEscuelaBase>> diccObjetosEscuela){
            
            if (diccObjetosEscuela == null)
            {
                throw new ArgumentNullException();
            }

            _diccionario = diccObjetosEscuela;
        }




        public IEnumerable<Evaluacion> GetEvaluaciones(){

            if(_diccionario.TryGetValue(LlavesDiccionario.Evaluaciones, out IEnumerable<ObjetoEscuelaBase> lista)){
                return lista.Cast<Evaluacion>();
            }else{
                return new List<Evaluacion>();
            }
        }

        //Sobrecargamos. Si no vamos a usar el out tenemos forzozamente que mandar un dumy
        public IEnumerable<string> GetAsignaturas(){
            return GetAsignaturas( out var dummy);
        }
        ////OUT es un parametro que tambien regresa el return 
        public IEnumerable<string> GetAsignaturas( out IEnumerable<Evaluacion> listaEvaluciones){

            listaEvaluciones = GetEvaluaciones();

            return (from evaluaciones in listaEvaluciones
                    where evaluaciones.Nota >= 3.0f
                    select evaluaciones.Nombre).Distinct();
        }


        public Dictionary<string, IEnumerable<Evaluacion>> GetEvaluacionAdig(){
            
            var dicRta = new Dictionary<string, IEnumerable<Evaluacion>>();

            var listaAsig = GetAsignaturas(out var listaEval);

            foreach (var asig in listaAsig)
            {
                var evalAsig = from eval in listaEval
                                where eval.Aignaturaa.Nombre == asig 
                                select eval;
                
                dicRta.Add(asig, evalAsig);
            }
            

            return dicRta;
        }




        public Dictionary<string, IEnumerable<object>> GetPromedioAlumnoPorAsignatura(){

            var respuesta = new Dictionary<string, IEnumerable<object>>();


            var diccEvaluacionesXAsignatura = GetEvaluacionAdig();
            foreach (var asigConEval in diccEvaluacionesXAsignatura)
            {
                var promAlum = from eval in asigConEval.Value
                            group eval by new {
                                            eval.Alumnoo.UniqueID,
                                            eval.Alumnoo.Nombre
                                        }
                            into groupEvalAlumno
                            select new TipoAlumnoPromedio{ 
                                AlumnoID = groupEvalAlumno.Key.UniqueID,
                                AlumnoNombre = groupEvalAlumno.Key.Nombre,
                                Promedio = groupEvalAlumno.Average(e => e.Nota)
                                };
                respuesta.Add(asigConEval.Key, promAlum);
            }

            return respuesta;
        }
    }
}