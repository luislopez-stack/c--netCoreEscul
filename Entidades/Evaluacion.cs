using System;

namespace NetCoreEscu.Entidades
{
    public class Evaluacion :ObjetoEscuelaBase
    {
        private Alumno _alumno;
        private Asignatura _asignatura;
        private float _nota;
        

        //PROPIEDADES
       public Asignatura Aignaturaa{ get; set;}
       public float Nota{ get; set;}
       public Alumno Alumnoo
        {
            get { return _alumno; }
            set { _alumno = value; }
        }
       public Asignatura asignatura
        {
            get { return _asignatura; }
            set { _asignatura = value; }
        }
        

        public Evaluacion(){
            _nota = Nota;
            _asignatura = asignatura;
            _alumno = Alumnoo;
            
        }

        public override string ToString()
        {
            return $"{_nota},{_alumno.Nombre}, {_asignatura.Nombre}";
        }
    }
}