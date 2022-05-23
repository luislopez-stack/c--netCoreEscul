using System;
using System.Collections.Generic;
using NetCoreEscu.Utils;

namespace NetCoreEscu.Entidades
{
    public class Curso : ObjetoEscuelaBase, ILugar
    {
        private string _jornada;
        private List<Asignatura> _asignatura;
        private List<Alumno> _alumno;



        public string Jornada {get; set;}
        public List<Asignatura> Asignaturaa
        {
            get { return _asignatura; }
            set { _asignatura = value; }
        }
        public List<Alumno> Alumno
        {
            get { return _alumno; }
            set { _alumno = value; }
        }

        public string Direccion { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Curso(){
            _jornada = Jornada;
        }

        public void BorrarDireccion()
        {
            Printer.DibujarLinea();
            Console.WriteLine("Limpiar establecimiento");
            Console.WriteLine($"Curso {Nombre} Limpio");
        }
    }

    
}