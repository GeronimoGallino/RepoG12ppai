using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAI2024.Entidades
{
    public class Usuario
    {
        //atributos
        public string contraseña;
        public string nombre;
        public bool premium;

        // metodo constructor
        public Usuario(string nom, string contra, bool prem)
        {
            nombre = nom;
            contraseña = contra;
            premium = false;
        }

        //propiedades get y set
        public string Contraseña { get { return contraseña;} set { contraseña = value; } }
        public string Nombre { get { return nombre; } set {  nombre = value; } }    
        public bool Premium { get { return premium; } set { premium = value; } }

      

    }
}
