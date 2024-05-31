using PPAI2024.Entidades;
using PPAI2024.Gestor_e_interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PPAI2024
{
    public partial class InterfazImportadorBodega : Form
    {
        GestorImportarBodega gestor = new GestorImportarBodega();
        InterfazAPIBodega API = new InterfazAPIBodega();
      
        private static List<Vino> listaConUnVino = new List<Vino>()
            {
                
                new Vino(new List<Maridaje> { new Maridaje("Carnes Rojas", "Ideal para carnes rojas a la parrilla", new TipoUva("Malbec", "Uva tinta")) }, new Bodega("Santa Julia", "H", "Vino blanco", 2, new DateTime(2024, 1, 27), new List<Vino>()), "2018", "2024-01-13", "Malbec Reserva", 250, 1, new List<Varietal> { new Varietal("Malbec", 100, new TipoUva("Malbec", "Uva tinta")) })
            };


        //Lista de Objetos Bodega para probar el metodo, la lista tiene que ser de las bodegas que tienen actualizacion disponible
        private static List<Bodega> listaDeBodegas = new List<Bodega>
        {
        new Bodega("Santa Julia", "H", "Vino blanco", 2, new DateTime(2024, 1, 27),listaConUnVino),
        new Bodega("Toro", "H", "Vino tinto", 4,new DateTime(2024, 2, 27), new List<Vino>()),
        new Bodega("El vino de la mona", "H", "Violeta", 3,new DateTime(2024, 3, 27), new List < Vino >()),
        new Bodega("Viña de balbo", "H", "Vino con cuerpo", 1,new DateTime(2024, 4, 27), new List < Vino >()),
        new Bodega("Benjamin", "H", "Vino dulce", 1,new DateTime(2024, 5, 27), new List < Vino >())
        };


        public InterfazImportadorBodega()
        {
            InitializeComponent();

            //MostrarLosVinosDeUnaBodega(listaDeBodegas);
            

            //Filtramos las bodegas que deben actualizarse 

            List<Bodega> bodegasParaActualizar = gestor.BuscarBodegasParaActualizar(listaDeBodegas);

            //mostramos las bodegas que deben actualizarse

            MostrarBodegasParaActualizar(bodegasParaActualizar);
          
        }

        public void MostrarBodegasParaActualizar(List<Bodega> bodegas)
        {
            // Limpiar las filas existentes en la grilla antes de agregar nuevas 
            grillaBodegas.Rows.Clear();

            // Iterar sobre la lista de bodegas
            foreach (var bod in bodegas)
            {
                // Crear una nueva fila
                DataGridViewRow fila = new DataGridViewRow();

                // Crear una celda y asignar el valor del nombre de la bodega
                DataGridViewTextBoxCell celdaNombre = new DataGridViewTextBoxCell
                {
                    Value = bod.Nombre
                };

                // Agregar la celda a la fila
                fila.Cells.Add(celdaNombre);

                // Agregar la fila a la grilla
                grillaBodegas.Rows.Add(fila);
            }
        }


        private void grillaBodegas_CellDoubleClick(object sender, DataGridViewCellEventArgs grilla)
        {
            if (grilla.RowIndex >= 0)
            {
                // Obtener el nombre de la bodega seleccionada por el usuario
                string nombreBodegaSeleccionada = grillaBodegas.Rows[grilla.RowIndex].Cells[0].Value.ToString();

                // Encontrar la bodega correspondiente en la lista de bodegas 
                Bodega bodegaSeleccionada = listaDeBodegas.Find(b => b.Nombre == nombreBodegaSeleccionada);

                if (bodegaSeleccionada != null)
                {
                    // Mostrar un cuadro de diálogo con botones de Aceptar y Cancelar
                    DialogResult dialogResult = MessageBox.Show(
                        $"Bodega seleccionada: {bodegaSeleccionada.Nombre}\n¿Desea continuar?",
                        "Confirmar selección",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question
                    );

                    //  el usuario hace clic en Aceptar
                    if (dialogResult == DialogResult.OK)
                    {
                        //ACA OBTENEMOS LAS ACTUALIZACIONES DE LA BODEGA SELECCIONADA 
                        List<Vino> listaActualizacionesVinos =  API.ObtenerActualizacionesBodega(bodegaSeleccionada);
                        
                        // ACA DE LA LISTA listaActualizacionesVinos creamos dos listas, una con los vinos a modificar y otra con los vinos paara crear
                        var (vinosParaActualizar, vinosParaCrear) = gestor.DeterminarVinosAActualizar(bodegaSeleccionada, listaActualizacionesVinos);

                        //ACA EMPEZARIA EL PASO 6 DEL CU
                        //Actualizar las novedades importadas del sistema de bodega
                        //y muestra un resumen de los vinos creados y/o actualizados 

                        //aca actualizamos y creamos los vinos pero no mostramos los datos modificados 
                        gestor.ActualizarOCrearVinos(vinosParaActualizar, vinosParaCrear, bodegaSeleccionada);

                        //Este metodo es para mostrar los vinos actualizados y creados, usamos como parametro listaActualizacionesVinos ya que ahi estan todos juntos
                        MostrarResumenVinosImportados(listaActualizacionesVinos);

                    }
                    else
                    {
                        // El usuario cancelo la opcion seleccionada 
                        MessageBox.Show("Selección cancelada.");
                    }
                }
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


        public void MostrarLosVinosDeUnaBodega(List<Bodega> listaBodegas)
        {
            foreach (var bodega in listaBodegas)
            {
                StringBuilder mensaje = new StringBuilder();
                mensaje.AppendLine($"Bodega: {bodega.Nombre}");
                if (bodega.Vino.Count == 0)
                {
                    mensaje.AppendLine("La bodega no tiene vinos.");
                }
                else
                {
                    foreach (var vino in bodega.Vino)
                    {
                        mensaje.AppendLine($"Nombre del vino: {vino.Nombre}, Precio: {vino.PrecioARS}, Nota de Cata: {vino.NotaDeCataBodega}");
                    }
                }
                mensaje.AppendLine(); // Línea en blanco para separar las bodegas
                MessageBox.Show(mensaje.ToString(), "Vinos de la Bodega");
            }
        }


    }
}
