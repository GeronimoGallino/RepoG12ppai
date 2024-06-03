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
        InterfazNotificacionPush InterfazNoti = new InterfazNotificacionPush();


        private static List<Vino> listaConUnVino = new List<Vino>()
            {
                
                new Vino(new List<Maridaje> { new Maridaje("Carnes Rojas", "Ideal para carnes rojas a la parrilla", new TipoUva("Malbec", "Uva tinta")) }, new Bodega("Santa Julia", "H", "Vino blanco", 2, new DateTime(2024, 1, 27), new List<Vino>()), "2018", "2024-01-13", "Malbec Reserva", 250, 1, new List<Varietal> { new Varietal("Malbec", 100, new TipoUva("Malbec", "Uva tinta")) })
            };


        //Lista de Objetos Bodega para probar el metodo, la lista tiene que ser de las bodegas que tienen actualizacion disponible
        private static List<Bodega> listaDeBodegas = new List<Bodega>
        {
        new Bodega("Santa Julia", "H", "Vino blanco", 1, new DateTime(2024, 1, 27),listaConUnVino),
        new Bodega("Toro", "H", "Vino tinto", 3,new DateTime(2024, 1, 27), new List<Vino>()),
        new Bodega("El vino de la mona", "H", "Violeta", 4,new DateTime(2024, 3, 27), new List < Vino >()),
        new Bodega("Viña de balbo", "H", "Vino con cuerpo",2,new DateTime(2024, 3, 27), new List < Vino >()),
        new Bodega("Benjamin", "H", "Vino dulce", 10,new DateTime(2024, 5, 27), new List < Vino >())
        };

        private static List<Enofilo> enofilos = new List<Enofilo>
            {
                new Enofilo(
                    new Vino(new List<Maridaje> { new Maridaje("Quesos", "Combina con quesos fuertes", new TipoUva("Cabernet Sauvignon", "Uva tinta")) }, listaDeBodegas[0], "2019", "2023-12-15", "Cabernet Sauvignon Reserva", 300, 1, new List<Varietal> { new Varietal("Cabernet Sauvignon", 100, new TipoUva("Cabernet Sauvignon", "Uva tinta")) }),
                    new List<Siguiendo> { new Siguiendo(null, listaDeBodegas[0], DateTime.Now.AddMonths(1), DateTime.Now) },
                    "Perez",
                    "Juan",
                    new Usuario("juanperez", "password", true)
                ),
                new Enofilo(
                    new Vino(new List<Maridaje> { new Maridaje("Pescados", "Ideal para pescados", new TipoUva("Chardonnay", "Uva blanca")) }, listaDeBodegas[1], "2020", "2024-01-10", "Chardonnay", 200, 1, new List<Varietal> { new Varietal("Chardonnay", 100, new TipoUva("Chardonnay", "Uva blanca")) }),
                    new List<Siguiendo> { new Siguiendo(null, listaDeBodegas[1], DateTime.Now.AddMonths(1), DateTime.Now) },
                    "Gomez",
                    "Ana",
                    new Usuario("anagomez", "password", false)
                ),
                new Enofilo(
                    new Vino(new List<Maridaje> { new Maridaje("Pastas", "Perfecto para pastas con salsa roja", new TipoUva("Merlot", "Uva tinta")) }, listaDeBodegas[2], "2018", "2024-02-20", "Merlot", 250, 1, new List<Varietal> { new Varietal("Merlot", 100, new TipoUva("Merlot", "Uva tinta")) }),
                    new List<Siguiendo> { new Siguiendo(null, listaDeBodegas[2], DateTime.Now.AddMonths(1), DateTime.Now) },
                    "Lopez",
                    "Carlos",
                    new Usuario("carloslopez", "password", true)
                ),
                new Enofilo(
                    new Vino(new List<Maridaje> { new Maridaje("Carnes Rojas", "Ideal para carnes rojas a la parrilla", new TipoUva("Malbec", "Uva tinta")) }, listaDeBodegas[3], "2021", "2024-03-05", "Malbec", 350, 1, new List<Varietal> { new Varietal("Malbec", 100, new TipoUva("Malbec", "Uva tinta")) }),
                    new List<Siguiendo>
                    {
                        new Siguiendo(null, listaDeBodegas[0], DateTime.Now.AddMonths(1), DateTime.Now),
                        new Siguiendo(null, listaDeBodegas[3], DateTime.Now.AddMonths(1), DateTime.Now)
                    },
                    "Martinez",
                    "Lucia",
                    new Usuario("luciamartinez", "password", false)
                )
            };





        public InterfazImportadorBodega()
        {
            InitializeComponent();
            opcImportarActVinos();
          
        }

        public void MostrarBodegasParaActualizar(List<Bodega> bodegas)
        {
            if (bodegas.Count == 0)
            {
                 MessageBox.Show("No hay Ninguna Bodega con Actualizacion Disponible");
                this.Hide();

            }
            else
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
                        tomarSelecBodega(bodegaSeleccionada, enofilos, this, API, InterfazNoti); 
                    }
                    else
                    {
                        // El usuario cancelo la opcion seleccionada 
                        MessageBox.Show("Selección cancelada.");
                    }
                }
            }
        }

        public void opcImportarActVinos()
        {
            habilitarPantalla();
        }

        public void habilitarPantalla()
        {
            this.Show();
            gestor.opcImportarActVinos(listaDeBodegas,this);
        }

        public void tomarSelecBodega( Bodega bodegaSeleccionada,List<Enofilo> enofilos,InterfazImportadorBodega interfaz,InterfazAPIBodega API,InterfazNotificacionPush InterfazNoti)
        {
            gestor.tomarSelecBodega(bodegaSeleccionada, enofilos, this, API, InterfazNoti);
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
                mensaje.AppendLine(); 
                MessageBox.Show(mensaje.ToString(), "Vinos de la Bodega");
            }
        }
        public void MostrarSeguidores(List<Enofilo> seguidores, Bodega bodega)
        {
            if (seguidores == null || seguidores.Count == 0)
            {
                MessageBox.Show("La bodega no tiene seguidores.", "Seguidores de la Bodega", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            StringBuilder mensaje = new StringBuilder();
            mensaje.AppendLine($"Seguidores de la bodega {bodega.Nombre}:\n");

            foreach (var seguidor in seguidores)
            {
                mensaje.AppendLine($"Nombre: {seguidor.Nombre} {seguidor.Apellido}");
                mensaje.AppendLine($"Usuario: {seguidor.Usuario.Nombre}");
                mensaje.AppendLine();
            }

            MessageBox.Show(mensaje.ToString(), "Seguidores de la Bodega", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }





    }
}
