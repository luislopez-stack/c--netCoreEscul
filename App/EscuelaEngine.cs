using System;
using System.Collections.Generic;
using System.Linq;
using NetCoreEscu.Entidades;
using NetCoreEscu.Utils;


namespace NetCoreEscu {
    
    public class EscuelaEngine{

        public Escuela escuela {get; set;}

        public void Inizializar(){

            //CREA INSTANCIA DE ESCUELA SE PASAN DATOS POR EL CONSTRUCTOR
            escuela = new Escuela("Saturnino", 2001, "valle", "Ags", "Mexico", TipoEscuela.Primaria.ToString());

            //PASAN DATOS POR PROPIEDADES
            escuela.TipoEscuela = TipoEscuela.Primaria.ToString();

            CargarCursos();
            CargarAsignaturas();
            CargarEvaluaciones();
            Console.WriteLine();
            
        }

#region Metodos de Carga
        private void CargarEvaluaciones()
        {
            var rnd = new Random();
            var lista = new List<Evaluacion>();

            foreach(var curso in escuela.Cursos){
                foreach(var asignatura in curso.Asignaturaa){
                    foreach(var alumno in curso.Alumno){
                        
                        for(int i = 0; i < 5; i++){
                            var evaluacion = new Evaluacion(){ 
                                        Aignaturaa = asignatura,
                                        Nombre = asignatura.Nombre +"EV# "+ i+1 , 
                                        Nota = (float)(5 * rnd.NextDouble()), 
                                        Alumnoo = alumno 
                            };
                            alumno.Evaluaciones.Add(evaluacion);
                        }
                    }
                }
            }
        }

        private void CargarAsignaturas()
        {
            foreach(var curso in escuela.Cursos){

                List<Asignatura> listaAsignatutas = new List<Asignatura>(){
                    new Asignatura(){Nombre= "Asignatura Matematicas"},
                    new Asignatura(){Nombre= "Asignatura Fisica"},
                    new Asignatura(){Nombre= "Asignatura Quimica"},
                    new Asignatura(){Nombre= "Asignatura Cuantica"}

                };
                curso.Asignaturaa = listaAsignatutas;
                //curso.Asignaturaa.AddRange(listaAsignatutas);
            }
        }

        private void CargarCursos(){
            //CREA LISTA DE CURSOS
            escuela.Cursos = new List<Curso>(){
                // CREA OBJETO CURSOS Y LOS DATOS SE PASAN POR PROPIEDADES
                new Curso(){Nombre = "Curso Basico", Jornada = TiposJornada.tiposJornada.Matutino.ToString()},
                new Curso(){Nombre = "Curso Intermedio", Jornada = TiposJornada.tiposJornada.Matutino.ToString()},
                new Curso(){Nombre = "Curso Avanzado", Jornada = TiposJornada.tiposJornada.Matutino.ToString()},
                new Curso(){Nombre = "Curso Maestria", Jornada = TiposJornada.tiposJornada.vespertino.ToString()}
            };

            //escuela.Cursos = listaCursos;  //ASIGNAN LOS CURSOS A LA ESCUELA PASADOS POR PROPIEDADES
            //escuela.Cursos.AddRange(listaCursos);   //Realizan lo mismo

            //AÑADE UN CURSO MAS
            escuela.Cursos.Add( new Curso(){Nombre = "Curso Calculo", Jornada = TiposJornada.tiposJornada.vespertino.ToString()} );


            Predicate<Curso> predicadoAlgoritmo = PredicadoObtieneNombreCurso; //Declara un delegado
            escuela.Cursos.RemoveAll(predicadoAlgoritmo);  // REMOVEALL ACEPTA DELEGADOS


            Random random = new Random();
            foreach(var curso in escuela.Cursos){

                curso.Alumno = GenerarAlumnos(random.Next(5,30));

            }

        }
#endregion
        
#region ListasDeObjetos

        /// METODO QUE OBTIENE TODOS LOS OBJETOS Y LOS GUARDA EN UNA LISTA 
        /// DEL TIPO BASE
        public List<ObjetoEscuelaBase> GetObjetosEscuela(){

            int countEvaluaciones=0;

            var listaObj = new List<ObjetoEscuelaBase>();
            listaObj.Add(escuela);
            listaObj.AddRange(escuela.Cursos);
            
            foreach (var curso in escuela.Cursos)
            {
                
                listaObj.AddRange(curso.Asignaturaa);
                listaObj.AddRange(curso.Alumno);
                
                foreach (var alumno in curso.Alumno)
                {
                    listaObj.AddRange(alumno.Evaluaciones);
                    countEvaluaciones += alumno.Evaluaciones.Count;
                }
            }
            return listaObj;
        }


        /// METODO QUE OBTIENE TODOS LOS OBJETOS Y LOS GUARDA EN UNA LISTA 
        /// DEL TIPO BASE
        /// PODEMOS RETORNAR UNA TUPLA
        /// EL TIPO IReadOnlyList EVITA AGREGAR NUEVOS ELEMENTOS A LA LISTA
        public (List<ObjetoEscuelaBase>, int) GetObjetosEscuela(bool traeEvaluaciones = true, bool traeAlunos = true, bool traeAsignaturas = true, bool traeCursos = true){

            int countEvaluaciones=0;

            var listaObj = new List<ObjetoEscuelaBase>();
            listaObj.Add(escuela);
            if (traeCursos)
                listaObj.AddRange(escuela.Cursos);
            
            foreach (var curso in escuela.Cursos)
            {
                if (traeAsignaturas)
                    listaObj.AddRange(curso.Asignaturaa);
                if (traeAlunos)
                    listaObj.AddRange(curso.Alumno);
                
                if(traeEvaluaciones){
                    foreach (var alumno in curso.Alumno)
                    {
                        listaObj.AddRange(alumno.Evaluaciones);
                        countEvaluaciones += alumno.Evaluaciones.Count;
                    }
                }
            }
            return (listaObj, countEvaluaciones);
        }

#endregion

#region Diccionario

        
        public Dictionary<LlavesDiccionario, IEnumerable<ObjetoEscuelaBase>> GetDiccionarioDeObjetos(){

            ///variables
            var listEvaluaciones = new List<ObjetoEscuelaBase>();
            var listAsignaturas = new List<ObjetoEscuelaBase>();
            var listAlumnos = new List<Alumno>();

            var diccionario = new Dictionary<LlavesDiccionario, IEnumerable<ObjetoEscuelaBase>>();
            diccionario.Add(LlavesDiccionario.Escuela, new[] {escuela});
            diccionario.Add(LlavesDiccionario.Cursos, escuela.Cursos.Cast<ObjetoEscuelaBase>());
            
            foreach(var curso in escuela.Cursos){
                listAlumnos.AddRange(curso.Alumno);
                listAsignaturas.AddRange(curso.Asignaturaa);
                foreach (var alum in curso.Alumno){
                    listEvaluaciones.AddRange(alum.Evaluaciones);
                }
            }
                diccionario.Add(LlavesDiccionario.Asignaturas, listAsignaturas.Cast<ObjetoEscuelaBase>());
                diccionario.Add(LlavesDiccionario.Alumnos, listAlumnos.Cast<ObjetoEscuelaBase>());
                diccionario.Add(LlavesDiccionario.Evaluaciones, listEvaluaciones.Cast<ObjetoEscuelaBase>());
            
            

            return diccionario;
        }


        public void ImprimirDiccionario(Dictionary<LlavesDiccionario, IEnumerable<ObjetoEscuelaBase>> dic, bool imprimirEval =  false){
            
            foreach (var obj in dic)
            {
                Printer.EscribeTitulo(obj.Key.ToString());
                
                
                foreach (var val in obj.Value)
                {
                    if (!imprimirEval){
                        if (val is Evaluacion){
                            continue;    
                        }
                    }

                    switch(obj.Key){
                        case LlavesDiccionario.Escuela:
                            Console.WriteLine("Escuela: " + val.Nombre);
                            break;
                        case LlavesDiccionario.Alumnos:
                            Console.WriteLine("Alumno: " + val.Nombre);
                            break;
                        case LlavesDiccionario.Cursos:
                            var temp = val as Curso;
                            var count = temp.Alumno.Count; 
                            Console.WriteLine(val.Nombre + " | ALUMNOS: " + count);
                            break;
                        default:
                            Console.WriteLine(val.Nombre);
                            break;
                    }
                    
                }
            }
        }

#endregion


        private List<Alumno> GenerarAlumnos(int cantidadSalon)
        {
            string[] nombres   = {"Alba","Eusebio","Felipa","Farid","Frausto","Nicol","Ana","Maria"};
            string[] apellido1 = {"Ruiz","Sarmiento","Ubier","Castro","Toledo","Madrigal","Ruiz","Herrera"};
            string[] nombres2 = {"Murr","Torres","Zambrano","Salas","Ponve","Aguirre","Sola","Rami"};

            var listaAlumnos = from nombre in nombres
                                from nombre2 in nombres2
                                from apellido in apellido1
                                select new Alumno(){ Nombre = $"{nombre} {nombre2} {apellido}" }; 

            return listaAlumnos.OrderBy(a => a.UniqueID).Take(cantidadSalon).ToList();
        }

        

        private static bool PredicadoObtieneNombreCurso(Curso curso){
            return curso.Nombre == "Curso Recreo";
        }

        
    }
}