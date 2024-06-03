using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAI2024.Entidades
{
    public class Enofilo
    {
        //atributos
        public string apellido;
        public string nombre;

        public Vino favorito;
        public Usuario usuario;
        public List<Siguiendo> seguido; // este deberia ser una lista pq puede seguir a mas de una bodega o somelier


        //metodo constructor
        public Enofilo(Vino fav,List<Siguiendo> seg, string apel, string nom,Usuario usu)
        {
            favorito = fav;
            usuario = usu;
            seguido = seg;
            apellido = apel;
            nombre = nom;
        }

        //propiedades get set
        public string Apellido { get { return apellido; } set { apellido = value; } }
        public string Nombre { get {  return nombre; } set {  nombre = value; } }
        public Vino Favorito { get {  return favorito; } set {  favorito = value; } }
        public Usuario Usuario { get {  return usuario; } set {  usuario = value; } }
       public List<Siguiendo> Seguido { get { return seguido; } set { seguido = value; } }

       
        public bool SeguisABodega(Bodega bodega)
        {
            foreach (var siguiendo in Seguido) //Recorremos la lista Seguido del Enofilo verificando si el enofilo sigue a la bodega seleccionada
            {
                if (siguiendo.SosDeBodega(bodega)) // le preguntamos a cada objeto Siguiendo si tiene como atributo a la bodega seleccionada
                {
                    return true;
                }
            }
            return false;
        }


        public string GetNombreUsuario()
        {
            return Usuario.Nombre;
        }

    }
}
