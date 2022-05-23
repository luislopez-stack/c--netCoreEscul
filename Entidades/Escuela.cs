using System;
using System.Collections.Generic;


namespace NetCoreEscu.Entidades{

    public class Escuela : ObjetoEscuelaBase, ILugar
    {
        //Encapsular campos en propiedades
        
        private string _direccion;
        private int _fundacion;
        private string _pais;
        private string _ciudad;
        private string _tipoEscuela;

 

        //PROPIEDADES
        // public string Nombre { 
        //     get{
        //         return _nombre;
        //     } 
        //     set{
        //         _nombre = value.ToUpper();
        //     } 
        // }
        public string Pais
        {
            get { return _pais; }
            set { _pais = value; }
        }
        public string Direccion { get; set; }
        public int Fundacion { get; set; }
        public string Ciudad { get; set;}
        public string TipoEscuela{ get; set; }
        public List<Curso> Cursos {get; set;}
        // {
        //     get { return _curso; }
        //     set { _curso = value; }
        // }
        
        public Escuela(string nombre, int fundacion,string direccion,string ciudad,string pais,string tipoEscuela){
            Nombre = nombre;
            _fundacion = fundacion;
            _direccion = Direccion;
            _ciudad = ciudad;
            _pais = pais;
            _tipoEscuela = tipoEscuela;
    
        }
        


        public void BorrarDireccion()
        {
            Utils.Printer.DibujarLinea();
            Console.WriteLine("Limpiar Escuela");
            foreach (var curso in Cursos)
            {
                curso.BorrarDireccion();
            }
            
        }
    }

}