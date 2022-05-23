using System;
using System.Collections.Generic;

namespace NetCoreEscu.Entidades
{
    public class Alumno : ObjetoEscuelaBase
    {

        //PROPIEDADES
        public List<Evaluacion> Evaluaciones {get; set;} = new List<Evaluacion>();


        public Alumno(){
          
        }
    }
}