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
             MostrarVinosACtualizadosDeUnaBodega(bodegaSeleccionada);
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
                        var nuevoVarietal = Vino.CrearVarietal(varietal.Descripcion, varietal.PorcentajeComposicion, tipoUvaEncontrado);
                        varietales.Add(nuevoVarietal);
                        
                        //varietales.Add(new Varietal(varietal.Descripcion, varietal.PorcentajeComposicion, tipoUvaEncontrado));
                    }
                }


                // SE CREA EL VINO NUEVO
                var nuevoVino = new Vino(maridajes, bodegaSeleccionada, vino.Añada, vino.FechaActualizacion, vino.Nombre, vino.PrecioARS, vino.NotaDeCataBodega, varietales);




                // SE AGREGA EL VINO CREADO A LA BODEGA SELECCIONADA
                bodegaSeleccionada.Vino.Add(nuevoVino);
            }

            // este metodo lo puse solo para mostrar los vinos que se agregaron a la lista de vinos, NO VA EN LA SOLUCION 
            //MostrarResumenVinosImportados(bodegaSeleccionada.Vino);
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
            return null; 
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
            return null;
        }

        public void MostrarVinosACtualizadosDeUnaBodega(Bodega bodega)
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

                    mensaje.AppendLine($"Nombre del vino ACTUALIZADO: {vino.Nombre}, Precio: {vino.PrecioARS}, Nota de Cata: {vino.NotaDeCataBodega}");
                }
            }
            mensaje.AppendLine(); // Línea en blanco para separar las bodegas
            MessageBox.Show(mensaje.ToString(), "Vinos de la Bodega");
        }


        //Invocacion del metodo buscar seguidores, capaz sobra lo del print VVVVVVVVVVVVVVVVVV
        public void BuscarSeguidoresBodega(Bodega bodega, List<Enofilo> enofilo)
        {
            List<String> seguidores = Enofilo.BuscarSeguidoresBodega(enofilo, bodega);
            Console.WriteLine($"Seguidores de la bodega {bodega.Nombre}:");
            foreach(var seguidor in seguidores)
            {
                Console.WriteLine(seguidor);
            }
        }

        public List<Enofilo> BuscarSeguidoresDeBodega(Bodega bodega, List<Enofilo> listaEnofilos)
        {
            List<Enofilo> seguidores = new List<Enofilo>();

            foreach (var enofilo in listaEnofilos) //error porque instancia al objeto como null
            {
                if (enofilo.SeguisABodega(bodega))
                {
                    seguidores.Add(enofilo);
                }
            }

            return seguidores;
        }

    }

}
