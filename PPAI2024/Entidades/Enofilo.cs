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

       
        public static List<string> BuscarSeguidoresBodega(List<Enofilo> enofilos, Bodega bodega)
        {
            List<string> seguidores = new List<string>();

            foreach (var enofilo in enofilos)
            {
                foreach (var siguiendo in enofilo.seguido)
                {
                    if (siguiendo.Bodega == bodega)
                    {
                        seguidores.Add($"{enofilo.Nombre} {enofilo.Apellido}");
                        break;
                    }
                }
            }

            return seguidores;
        }
        public bool SeguisABodega(Bodega bodega)
        {
            foreach (var siguiendo in Seguido)
            {
                if (siguiendo.SosDeBodega(bodega))
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
