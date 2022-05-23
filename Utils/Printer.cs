using System;

namespace NetCoreEscu.Utils
{
    public static class Printer
    {
        public static void DibujarLinea(int tamano = 10){
            var linea = "".PadLeft(tamano, '=');
            Console.WriteLine(linea);
        }
        
        public static void EscribeTitulo(string titulo){
            DibujarLinea(titulo.Length + 4);
            Console.WriteLine("|" + titulo.ToUpper() + "|");
            DibujarLinea(titulo.Length + 4);
        }
    }
}