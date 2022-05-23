using System;

namespace NetCoreEscu.Entidades
{
    public abstract class ObjetoEscuelaBase
    {
        private string _uniqueID;
        private string _nombre;
        //private List<Evaluaciones> _evaluaciones;
        
        

        //PROPIEDADES
        public string Nombre {get; set;}
        public string UniqueID {get; set;} = Guid.NewGuid().ToString();


        public ObjetoEscuelaBase(){
            _nombre = Nombre;
            _uniqueID = UniqueID;
        }

        public override string ToString()
        {
            return $"{_nombre},{_uniqueID}";
        }
    }
}