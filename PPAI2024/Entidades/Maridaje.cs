using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAI2024.Entidades
{
    public class Maridaje
    {
        //atributos
        public string descripcion;
        public string nombre;

        // metodo constructor
        public Maridaje(string desc, string nom)
        {
            nombre = nom;
            descripcion = desc;
        }

        // propiedades get y set
        public string Descripcion { get { return descripcion; } set {  descripcion = value; } }
        public string Nombre { get {  return nombre; } set {  nombre = value; } }
    }
}
