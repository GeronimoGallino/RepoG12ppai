using PPAI2024.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAI2024
{
    internal class GestorImportarBodega
    {
        public List<Bodega> BuscarBodegasParaActualizar(List<Bodega> listaDeBodegas)
        {
            List<Bodega> bodegasParaActualizar = new List<Bodega>();

            // Recorrer la lista de bodegas
            foreach (var bodega in listaDeBodegas)
            {
                // Aplicar el método EstaParaActuNovedadesVino() a cada bodega
                if (bodega.EstaParaActuNovedadesVino())
                {
                    // Si el método devuelve true, agregar la bodega a la lista de bodegas para actualizar
                    bodegasParaActualizar.Add(bodega);
                }
            }

            // Devolver la lista de bodegas para actualizar
            return bodegasParaActualizar;
        }
        public (List<Vino> vinosParaActualizar, List<Vino> vinosParaCrear) DeterminarVinosAActualizar(Bodega bodega, List<Vino> listaActualizacionesVinos)
        {
            List<Vino> vinosParaActualizar = new List<Vino>();
            List<Vino> vinosParaCrear = new List<Vino>();

            foreach (var vino in listaActualizacionesVinos)
            {
                if (bodega.tenesEsteVino(vino))
                {
                    vinosParaActualizar.Add(vino);
                }
                else
                {
                    vinosParaCrear.Add(vino);
                }
            }

            return (vinosParaActualizar, vinosParaCrear);
        }
    }

}
