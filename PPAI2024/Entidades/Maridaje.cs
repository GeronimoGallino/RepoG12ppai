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
        public TipoUva tipoUva;

        // metodo constructor
        public Maridaje(string desc, string nom,TipoUva tu )
        {
            nombre = nom;
            descripcion = desc;
            tipoUva = tu;
        }

        // propiedades get y set
        public string Descripcion { get { return descripcion; } set {  descripcion = value; } }
        public string Nombre { get {  return nombre; } set {  nombre = value; } }
        public TipoUva TipoUva { get { return tipoUva; } set { tipoUva = value; } }


        public bool SosMaridaje(string nombreMaridaje)
        {
            // Comparamos el nombre del maridaje con el nombre pasado por parámetro
            return this.Nombre == nombreMaridaje;
        }





    }
}
