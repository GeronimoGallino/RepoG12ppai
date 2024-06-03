using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAI2024.Entidades
{
    public class Vino
    {
        //atributos por referencia 
        private string añada;
        private string fechaActualizacion;
        private string nombre;
        private int notaDeCataBodega;
        private int precioARS;

        //atributos por referencia 
        private List<Maridaje> maridaje; 
        private Bodega bodega;
        private List<Varietal> varietal;

        //metodo constructor
        public Vino(List<Maridaje> mari, Bodega bode, string aña, string fecha, string nom, int precio, int nota, List<Varietal> vari)
        {
            
            añada = aña;
            fechaActualizacion = fecha;
            nombre = nom;
            precioARS = precio;
            notaDeCataBodega = nota;
            maridaje = mari;
            bodega = bode;
            varietal = vari;
        }

        //propiedades get y set
        public string Añada { get { return añada; } set { añada = value; } }
        public string FechaActualizacion { get {  return fechaActualizacion; } set {  fechaActualizacion = value; } }
        public string Nombre { get {  return nombre; } set {  nombre = value; } }
        public int PrecioARS { get { return precioARS; } set { precioARS = value; } }
        public int NotaDeCataBodega { get {  return notaDeCataBodega; } set { notaDeCataBodega = value ; } }
        public List<Maridaje> Maridaje { get {  return maridaje; } set {  maridaje = value; } }
        public Bodega Bodega { get { return bodega; } set { bodega = value; } }
        public List<Varietal> Varietal { get {  return varietal; } set {  varietal = value; } }


        public bool sosEsteVino(Vino otroVino)
        {
            return this.Nombre == otroVino.Nombre && this.Añada == otroVino.Añada;
        }


        //Método para crear Varietal
        public static Varietal CrearVarietal(string descripcion, int porcentajeComposicion, TipoUva tipoUva)
        {
            return new Varietal(descripcion, porcentajeComposicion, tipoUva);
        }

    }
}
