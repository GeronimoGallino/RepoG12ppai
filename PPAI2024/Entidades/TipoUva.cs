using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAI2024.Entidades
{
    public class TipoUva
    {
        //atributos
        public string descripcion;
        public string nombre;

        //metodo constructor
         public TipoUva(string desc, string nom)
         {
            descripcion = desc;
            nombre = nom;
         }

        //propiedades get y set
        public string Descripcion { get { return descripcion; } set { descripcion = value; } }
        public string Nombre { get { return nombre; } set { nombre = value; } }

        public bool SosTipoUva(string nombreTipoUva)
        {
            // Comparamos el nombre del tipo de uva con el nombre pasado por parámetro
            return this.Nombre == nombreTipoUva;
        }

    }
}
