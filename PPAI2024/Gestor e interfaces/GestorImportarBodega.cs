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
        private List<Maridaje> maridajes;
        private List<TipoUva> tiposDeUva;

        public GestorImportarBodega()
        {
            // Inicializar listas
            maridajes = new List<Maridaje>
        {
            new Maridaje("Carne Asada", "Perfecto para acompañar con carne asada", new TipoUva("Tinto y jugoso, ideal para carnes rojas", "Malbec")),
            new Maridaje("Quesos Fuertes", "Marida bien con quesos fuertes y curados", new TipoUva("Potente y con cuerpo", "Cabernet Sauvignon")),
            new Maridaje("Pasta con Salsa de Tomate", "Ideal con platos de pasta y salsas de tomate", new TipoUva("Fresco y afrutado", "Sangiovese")),
            new Maridaje("Pescado y Mariscos", "Perfecto para pescados y mariscos", new TipoUva("Ligero y cítrico", "Sauvignon Blanc"))
        };

            tiposDeUva = new List<TipoUva>
        {
            new TipoUva("Tinto y jugoso, ideal para carnes rojas", "Malbec"),
            new TipoUva("Potente y con cuerpo", "Cabernet Sauvignon"),
            new TipoUva("Fresco y afrutado", "Sangiovese"),
            new TipoUva("Ligero y cítrico", "Sauvignon Blanc")
        };
        }




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

        //private void CrearNuevosVinos(List<Vino> vinosParaCrear, Bodega bodegaSeleccionada)
        //{
        //    foreach (var vino in vinosParaCrear)
        //    {





        //        // Aquí puedes agregar el vino a la bodega.
        //        bodegaSeleccionada.Vino.Add(vino);
        //    }
        //}





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

        private void CrearNuevosVinos(List<Vino> vinosParaCrear, Bodega bodegaSeleccionada)
        {
            foreach (var vino in vinosParaCrear)
            {
                var maridajes = new List<Maridaje>();
                foreach (var maridaje in vino.Maridaje)
                {
                    var encontrado = BuscarMaridaje(maridaje.Nombre);
                    if (encontrado != null)
                    {
                        maridajes.Add(encontrado);
                    }
                }

                var varietales = new List<Varietal>();
                foreach (var varietal in vino.Varietal)
                {
                    var tipoUvaEncontrado = BuscarTipoUva(varietal.TipoUva.Nombre);
                    if (tipoUvaEncontrado != null)
                    {
                        varietales.Add(new Varietal(varietal.Descripcion, varietal.PorcentajeComposicion, tipoUvaEncontrado));
                    }
                }


                // SE CREA EL VINO NUEVO
                var nuevoVino = new Vino(maridajes, bodegaSeleccionada, vino.Añada, vino.FechaActualizacion, vino.Nombre, vino.PrecioARS, vino.NotaDeCataBodega, varietales);

                // SE AGREGA EL VINO CREADO A LA BODEGA SELECCIONADA
                bodegaSeleccionada.Vino.Add(nuevoVino);
            }
        }









        public Maridaje BuscarMaridaje(string nombreMaridaje)
        {
            foreach (var maridaje in maridajes)
            {
                if (maridaje.SosMaridaje(nombreMaridaje))
                {
                    return maridaje;
                }
            }
            return null; // O manejar el caso de no encontrar el maridaje
        }

        public TipoUva BuscarTipoUva(string nombreTipoUva)
        {
            foreach (var tipoUva in tiposDeUva)
            {
                if (tipoUva.SosTipoUva(nombreTipoUva))
                {
                    return tipoUva;
                }
            }
            return null; // O manejar el caso de no encontrar el tipo de uva
        }



        


    }

}
