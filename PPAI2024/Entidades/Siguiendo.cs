using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAI2024.Entidades
{
    public class Siguiendo
    {
        //atributos
        public DateTime fechaFin;
        public DateTime fechaInicio;

        public Enofilo amigo;
        public Bodega bodega;

        //constructor
        public Siguiendo(Enofilo ami, Bodega bode, DateTime fechaF,DateTime fechaI)
        {
            amigo = ami;
            bodega = bode;
            fechaFin = fechaF;
            FechaInicio = fechaI;
        }
        //propiedades get y set
        public DateTime FechaFin { get { return fechaFin; } set { fechaFin = value; } }
        public DateTime FechaInicio { get {  return fechaInicio; } set { fechaInicio = value; } }
        public Enofilo Amigo { get { return amigo; } set { amigo = value; } }
        public Bodega Bodega { get {  return bodega; } set {  bodega = value; } }

        public bool SosDeBodega(Bodega bodega)
        {
            return this.bodega.Nombre == bodega.Nombre;
        }
    }

}
