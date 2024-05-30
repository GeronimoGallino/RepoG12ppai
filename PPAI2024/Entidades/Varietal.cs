using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAI2024.Entidades
{
    public class Varietal
    {

        //Atributos propios 
        public string descripcion;
        public int porcentajeComposicion;

        //Atributos por referencia
        public TipoUva tipoUva;

        Varietal(string descrip, int porComp, TipoUva tU)
        {
            descripcion = descrip;
            porcentajeComposicion = porComp;
            tipoUva = tU;
        }


        public string Descripcion { get { return descripcion; } set { descripcion = value; } }
        public int PorcentajeComposicion { get { return porcentajeComposicion; } set { porcentajeComposicion = value; } }
        public TipoUva TipoUva { get { return tipoUva; } set { tipoUva = value; } }
    }
}
