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
                // Si no se ha establecido, se considera que hay actualizaciones disponibles
                return true;
            }
            else
            {
                // Calcular el período desde la última actualización hasta ahora
                TimeSpan tiempoDesdeUltimaActualizacion = DateTime.Now - fechaUltimaActualizacion;

                // Verificar si el tiempo transcurrido supera el período de actualización esperado
                if (tiempoDesdeUltimaActualizacion.TotalDays >= periodoActualizacion)
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
            return Vino.Any(v => v.sosEsteVino(vino));
        }
    }
}
