using PPAI2024.Entidades;
using PPAI2024.Gestor_e_interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PPAI2024
{
    public partial class InterfazImportadorBodega : Form
    {
        GestorImportarBodega gestor = new GestorImportarBodega();
        InterfazAPIBodega API = new InterfazAPIBodega();
        //List<Vino> vinoParaActualizar = new List<Vino>();
        //List<Vino> vinoParaCrear = new List<Vino>();
        //Lista de Objetos Bodega para probar el metodo, la lista tiene que ser de las bodegas que tienen actualizacion disponible
        private static List<Bodega> listaDeBodegas = new List<Bodega>
        {
        new Bodega("Santa Julia", "H", "Vino blanco", 60, new DateTime(2024, 1, 27), new List<Vino>()),
        new Bodega("Toro", "H", "Vino tinto", 40,new DateTime(2024, 2, 27), new List<Vino>()),
        new Bodega("El vino de la mona", "H", "Violeta", 30,new DateTime(2024, 3, 27), new List < Vino >()),
        new Bodega("Viña de balbo", "H", "Vino con cuerpo", 120,new DateTime(2024, 4, 27), new List < Vino >()),
        new Bodega("Benjamin", "H", "Vino dulce", 30,new DateTime(2024, 5, 27), new List < Vino >())
        };
        public InterfazImportadorBodega()
        {
            InitializeComponent();
            //Filtramos las bodegas que deben actualizarse 

            //List<Bodega> bodegasParaActualizar = gestor.BuscarBodegasParaActualizar(listaDeBodegas);

            //mostramos las bodegas que deben actualizarse

            //MostrarBodegasParaActualizar(bodegasParaActualizar);




            // Crear tipos de uva
            TipoUva tipoUvaMalbec = new TipoUva("Una de las uvas más representativas de Argentina", "Malbec");
            TipoUva tipoUvaCabernetSauvignon = new TipoUva("Una variedad clásica", "Cabernet Sauvignon");
            TipoUva tipoUvaChardonnay = new TipoUva("Uva blanca muy popular", "Chardonnay");

            // Crear varietales
            Varietal varietalMalbec = new Varietal("Varietal Malbec", 100, tipoUvaMalbec);
            Varietal varietalCabernetSauvignon = new Varietal("Varietal Cabernet Sauvignon", 100, tipoUvaCabernetSauvignon);
            Varietal varietalChardonnay = new Varietal("Varietal Chardonnay", 100, tipoUvaChardonnay);

            // Crear maridajes
            Maridaje maridajeCarne = new Maridaje("Carne", "Ideal para carnes rojas", tipoUvaMalbec);
            Maridaje maridajeQueso = new Maridaje("Queso", "Combina bien con quesos fuertes", tipoUvaCabernetSauvignon);
            Maridaje maridajePescado = new Maridaje("Pescado", "Perfecto para pescados y mariscos", tipoUvaChardonnay);

            // Crear la bodega Crespo
            Bodega bodegaCrespo = new Bodega("Crespo", "Historia de la bodega Crespo", "Una descripción de la bodega Crespo", 30, DateTime.Now, new List<Vino>());
            // Crear lista de vinos
            List<Vino> listaVinosCrespo = new List<Vino>
                {
                    new Vino(new List<Maridaje> { maridajeCarne }, bodegaCrespo, "2018", "2024-05-28", "Malbec Reserva", 1500, 90, new List<Varietal> { varietalMalbec }),
                    new Vino(new List<Maridaje> { maridajeQueso }, bodegaCrespo, "2019", "2024-05-28", "Cabernet Sauvignon Gran Reserva", 2000, 92, new List<Varietal> { varietalCabernetSauvignon }),
                    new Vino(new List<Maridaje> { maridajePescado }, bodegaCrespo, "2020", "2024-05-28", "Chardonnay", 1200, 88, new List<Varietal> { varietalChardonnay })
                };
            gestor.MostrarResumenVinosImportados(listaVinosCrespo);
            //            // Crear la lista de vinos
            //           List<Vino> listaDeVinosCrespo = new List<Vino>
            //{
            //    new Vino(
            //                new List<Maridaje> { maridajeCarne, maridajeQueso },
            //                bodegaCrespo,
            //                "2018",
            //                "2024-01-01",
            //                "Crespo Malbec",
            //                750,
            //                95,
            //                new List<Varietal> { varietalMalbec }
            //            ),
            //    new Vino(
            //                new List<Maridaje> { maridajeQueso, maridajePescado },
            //                bodegaCrespo,
            //                "2019",
            //                "2024-02-01",
            //                "Crespo Cabernet Sauvignon",
            //                820,
            //                92,
            //                new List<Varietal> { varietalCabernetSauvignon }
            //            ),
            //    new Vino(
            //                new List<Maridaje> { maridajePescado },
            //                bodegaCrespo,
            //                "2020",
            //                "2024-03-01",
            //                "Crespo Chardonnay",
            //                680,
            //                90,
            //                new List<Varietal> { varietalChardonnay }
            //            )
            //};

            //            // Asignar la lista de vinos a la bodega Crespo
            //            bodegaCrespo.Vino = listaDeVinosCrespo;

            gestor.MostrarResumenVinosImportados(listaVinosCrespo);
        }



        // este metodo era para mostrar una sola bodega
        //public void MostrarBodegaParaActualizar(Bodega bod)
        //{


        //    DataGridViewRow fila = new DataGridViewRow();

        //    DataGridViewTextBoxCell celdaNombre = new DataGridViewTextBoxCell();

        //    celdaNombre.Value = bod.Nombre;

        //    fila.Cells.Add(celdaNombre);

        //    grillaBodegas.Rows.Add(fila);
        //}


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
                       // gestor.MostrarResumenVinosImportados(listaActualizacionesVinos);

                    }
                    else
                    {
                        // El usuario cancelo la opcion seleccionada 
                        MessageBox.Show("Selección cancelada.");
                    }
                }
            }
        }

    }
}
