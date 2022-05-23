using System;
using System.Collections.Generic;
using System.Linq;
using NetCoreEscu.Entidades;
using NetCoreEscu.Utils;

namespace NetCoreEscu
{
    
    class Program
    {
        static void Main(string[] args)
        {
            var engine = new EscuelaEngine();
            engine.Inizializar();
            Printer.EscribeTitulo("BIENVENIDO ESCUELA");
            imprimirCursosEscuela(engine.escuela);  //CLASE ESTATICA(NO OCUPA INSTANCIA) PARA IMPRIMIR CURSOS

            
            
            Printer.DibujarLinea();
            Printer.EscribeTitulo("Polimorfismo");
            
            var listaObjetos = engine.GetObjetosEscuela();  //CREA UNA LISTA TOTAL DE TODOS LOS OBJETOS 

            //OBTENER TODOS LOS OBJETOS QUE TENGAN LA INTERFAS ILUGAR
            //MEDIANTE LAS INTERFACES PODEMOS AGRUPAR OBJETOS
            var listaIlugar = from obj in listaObjetos
                                where obj is ILugar
                                select (ILugar)obj;


            engine.escuela.BorrarDireccion();

            
            Printer.DibujarLinea();
            Printer.EscribeTitulo("Diccionario");


            //GUARDAMOS TODOS LOS OBJETOS EN UN DICCIONARO
            var diccionarioDeObjetos = engine.GetDiccionarioDeObjetos();


            //IMPRIMIMOS EL DICCIONARIO
            engine.ImprimirDiccionario(diccionarioDeObjetos, true);


            //DICCIONARIO.UNA LISTA ACOMPAÑADA DE UN INDICE
            Dictionary<int, string> diccionario = new Dictionary<int, string>();

            //dICCIONARIO AGREGA DATOS
            diccionario.Add(10,"Lorem");
            diccionario.Add(11,"Inpsum");
            diccionario[12] = "Ensump";


            Console.WriteLine(diccionario[11]);


            Printer.EscribeTitulo("Reporteador");
            var reporteador = new Reporteador(diccionarioDeObjetos);
            var lisEvaluaciones = reporteador.GetEvaluaciones();
            var lisAsignaturas = reporteador.GetAsignaturas();
            var lisEvaluacionesXAsignatura = reporteador.GetEvaluacionAdig();
            var lisPromedioXAsignatura = reporteador.GetPromedioAlumnoPorAsignatura();
            // foreach (var item in lisPromedioXAsignatura)
            // {
            //     foreach (var alum in item.Value)
            //     {
            //         var tmp = alum as Alumno;
            //     }
            // }
        }


        private static void imprimirCursosEscuela(Escuela escuela){

            Printer.EscribeTitulo("CURSOS ESCUELA");

            foreach(var curso in escuela.Cursos){
                Console.WriteLine($" {curso.Nombre} , ID: {curso.UniqueID}, Turno: {curso.Jornada}");
            }
        }

        
    }
}
