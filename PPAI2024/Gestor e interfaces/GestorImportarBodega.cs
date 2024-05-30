using PPAI2024.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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



        // eSTOS 3 METODOS EN SUSPENSO
        // ESTO SERIA LO QUE INVOCA AL ALT
        public void ActualizarOCrearVinos(List<Vino> vinosParaActualizar, List<Vino> vinosParaCrear, Bodega bodegaSeleccionada)
        {
            ActualizarCaracteristicasVinosExistentes(vinosParaActualizar, bodegaSeleccionada);
            CrearNuevosVinos(vinosParaCrear, bodegaSeleccionada);
        }


        private void ActualizarCaracteristicasVinosExistentes(List<Vino> vinosParaActualizar, Bodega bodegaSeleccionada)
        {
            foreach (var vino in vinosParaActualizar)
            {
                bodegaSeleccionada.ActualizarDatosVino(vino);
            }
        }

        private void CrearNuevosVinos(List<Vino> vinosParaCrear, Bodega bodegaSeleccionada)
        {
            foreach (var vino in vinosParaCrear)
            {





                // Aquí puedes agregar el vino a la bodega.
                bodegaSeleccionada.Vino.Add(vino);
            }
        }
        public void MostrarResumenVinosImportados(List<Vino> vinos)
        {
            if (vinos == null || vinos.Count == 0)
            {
                MessageBox.Show("No hay vinos para mostrar.");
                return;
            }

            // Asumimos que todos los vinos en la lista pertenecen a la misma bodega pq la lista viene de la API de una bodega
            Bodega bodega = vinos[0].Bodega;
            string mensaje = $"Bodega: {bodega.Nombre}\n";

            foreach (var vino in vinos)
            {
                string maridajes = string.Join(", ", vino.Maridaje.ConvertAll(m => $"{m.Nombre} ({m.Descripcion})"));
                string varietales = string.Join(", ", vino.Varietal.ConvertAll(v => $"{v.TipoUva.Nombre} ({v.PorcentajeComposicion}%)"));

                mensaje += $"Nombre: {vino.Nombre}\n" +
                           $"Añada: {vino.Añada}\n" +
                           $"Nota de Cata: {vino.NotaDeCataBodega}\n" +
                           $"Precio: {vino.PrecioARS} ARS\n" +
                           $"Maridajes: {maridajes}\n" +
                           $"Varietales: {varietales}\n\n";
            }

            MessageBox.Show(mensaje, "Resumen de Vinos Importados");
        }



    }

}
