using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAI2024.Entidades
{
    public class Bodega
    {
        
        //atributos
        private string descripcion;
        private string historia;
        private string nombre;
        private int periodoActualizacion;
        private DateTime fechaUltimaActualizacion;
        private List<Vino> vino;

        //metodo constructor
        public Bodega(string nom, string hist, string descrip, int peract, DateTime fechaUltACt, List<Vino> vin)
        {
            nombre = nom;
            historia = hist;
            descripcion = descrip;
            periodoActualizacion = peract;
            fechaUltimaActualizacion = fechaUltACt;
            vino = vin;
        }

      
        //propiedades get set

        public string Descripcion { get { return descripcion; } set { descripcion = value; } }
        public string Historia { get { return historia; } set { historia = value; } }
        public string Nombre { get { return nombre; } set { nombre = value; } }
        public int PeriodoActualizacion { get { return periodoActualizacion; } set { periodoActualizacion = value; } }
        public DateTime FechaUltimaActualizacion { get { return fechaUltimaActualizacion; } set { fechaUltimaActualizacion = value; } }
        public List<Vino> Vino { get { return vino;  } set { vino = value; } }

        public bool EstaParaActuNovedadesVino()
        {
            // Verificar si se ha establecido una fecha de última actualización
            if (fechaUltimaActualizacion == default(DateTime))
            {
                // Si no se ha establecido, se considera  que nunca tuvo actualizacion por lo tanto hay actualizaciones disponibles
                return true;
            }
            else
            {
                // Calcular la diferencia en meses entre la última actualización y la fecha actual
                int mesesDesdeUltimaActualizacion = ((DateTime.Now.Year - fechaUltimaActualizacion.Year) * 12) + DateTime.Now.Month - fechaUltimaActualizacion.Month;

                // Verificar si el tiempo transcurrido en meses supera el período de actualización esperado
                if (mesesDesdeUltimaActualizacion >= periodoActualizacion)
                {
                    return true; // Hay actualizaciones disponibles
                }
                else
                {
                    return false; // No hay actualizaciones disponibles
                }
            }
        }

        public bool tenesEsteVino(Vino vino)
        {
            foreach (var v in Vino) //Recorremos Los vinos de la bodega y a cada uno le preguntamos si es igual al vino pasado por parametro
            {
                if (v.sosEsteVino(vino))
                {
                    return true;
                }
            }
            return false;
        }
                        
        public void ActualizarDatosVino(Vino vinoActualizado) //Entra como parametro el vino con los datos Actualizados
        {
            
            foreach (var vino in vino) //Recorre cada vino de la bodega preguntando cual es el que tiene que actualizar
            {
                if (vino.sosEsteVino(vinoActualizado))  //Aca preguntamos si el vino que tiene la bodega es igual al vino que tenemos los datos actulizados,true= se actualiza
                {
                    vino.PrecioARS = vinoActualizado.PrecioARS;
                    vino.NotaDeCataBodega = vinoActualizado.NotaDeCataBodega;
                    vino.FechaActualizacion = vinoActualizado.FechaActualizacion; //el vino actualizado viene con la fecha del dia que llamamos a la API
                    break; //no hace falta seguir recorriendo el resto de vinos de una bodega pq ya encontramos al que hay que actualizarle los datos
                }
            }
        }

        public void SetFechaUltimaActualizacion()
        {
            fechaUltimaActualizacion = DateTime.Now;
        }

        public void crearVino(List<Maridaje> maridajes, string añada, string fechaActualizacion, string nombre, int precioARS, int notaDeCataBodega, List<Varietal> varietales)
        {
            Vino nuevoVino = new Vino(maridajes, this, añada, fechaActualizacion, nombre, precioARS, notaDeCataBodega, varietales);
            this.Vino.Add(nuevoVino); // Agrega el nuevo vino a la lista de vinos de la bodega
        }

    }
}
