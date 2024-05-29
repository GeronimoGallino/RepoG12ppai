using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAI2024.Entidades
{
    internal class Enofilo
    {
        //atributos
        public string apellido;
        public string nombre;

        public Vino favorito;
        public Usuario usuario;
        public Siguiendo seguido; // este deberia ser una lista pq puede seguir a mas de una bodega o somelier


        //metodo constructor
        public Enofilo(Vino fav,Siguiendo seg, string apel, string nom,Usuario usu)
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
        public Siguiendo Siguiendo { get {  return seguido; } set {  seguido = value; } }
    }
}
