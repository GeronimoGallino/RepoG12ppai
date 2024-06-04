using PPAI2024.Entidades;
using PPAI2024.Gestor_e_interfaces;
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

        public void opcImportarActVinos(List<Bodega> listaDeBodegas, InterfazImportadorBodega interfaz)
        {

            //Filtramos las bodegas que deben actualizarse 

            List<Bodega> bodegasParaActualizar = BuscarBodegasParaActualizar(listaDeBodegas);

            //mostramos las bodegas que deben actualizarse

            interfaz.MostrarBodegasParaActualizar(bodegasParaActualizar);

        }

        public void tomarSelecBodega(Bodega bodegaSeleccionada,List<Enofilo> enofilos, InterfazImportadorBodega interfaz, InterfazAPIBodega API, InterfazNotificacionPush interfazNoti)
        {
            //ACA OBTENEMOS LAS ACTUALIZACIONES DE LA BODEGA SELECCIONADA 
            List<Vino> listaActualizacionesVinos = obtenerActVinosBodega(API,bodegaSeleccionada);

            // ACA DE LA LISTA listaActualizacionesVinos creamos dos listas, una con los vinos a modificar y otra con los vinos paara crear
            var (vinosParaActualizar, vinosParaCrear) = determinarVinosAActualizar(bodegaSeleccionada, listaActualizacionesVinos);

            //ACA EMPEZARIA EL PASO 6 DEL CU 

            //aca actualizamos y creamos los vinos pero no mostramos los datos modificados 
            ActualizarOCrearVinos(vinosParaActualizar, vinosParaCrear, bodegaSeleccionada);


            //actualizamos la ultima fecha de actualizacion de la bodega 
            bodegaSeleccionada.SetFechaUltimaActualizacion();

            //Este metodo es para mostrar los vinos actualizados y creados, usamos como parametro listaActualizacionesVinos ya que ahi estan todos juntos
            interfaz.MostrarResumenVinosImportados(listaActualizacionesVinos);

            //Enviamos Notificaciones a los enofilos (Paso 7 del CU)
            List<Enofilo> seguidoresBodegaSelec = buscarSeguidoresDeBodega(bodegaSeleccionada, enofilos);
            
            interfazNoti.NotificarNovedadVinoParaBodega(seguidoresBodegaSelec, bodegaSeleccionada);
        }


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
            foreach (var vino in vinosParaCrear) //aca Iteramos sobre cada vino para crear 
            {
                var maridajes = new List<Maridaje>();
                foreach (var maridaje in vino.Maridaje) //Iteramos sobre la lista de Maridaje de cada vino 
                {
                    var maridajeEncontrado = BuscarMaridaje(maridaje.Nombre);
                    if (maridajeEncontrado != null)
                    {
                        maridajes.Add(maridajeEncontrado); //Si encuentra un Objeto Maridaje igual al nombre del Maridaje pasado por parametro(el que viene por la API)
                                                           //lo agrega a una lista de maridajes que luego van a ser agregados en la creacion del vino
                    }
                }

                var varietales = new List<Varietal>(); 
                foreach (var varietal in vino.Varietal)  //Iteramos sobre la lista de TipoUva de cada vino
                {
                    var tipoUvaEncontrado = BuscarTipoUva(varietal.TipoUva.Nombre);  //Por cada objeto tipo uva encontrado el vino crea un nuevo varietal
                                                                                     // (patronCreador) con el objeto TipoUva que se pasa por parametro
                    if (tipoUvaEncontrado != null)                             
                    {
                        var nuevoVarietal = Vino.CrearVarietal(varietal.Descripcion, varietal.PorcentajeComposicion, tipoUvaEncontrado);
                        varietales.Add(nuevoVarietal);
          
                    }
                }

                
                // SE CREA EL VINO NUEVO
                crearVino(bodegaSeleccionada, maridajes, vino.Añada, vino.FechaActualizacion, vino.Nombre, vino.PrecioARS, vino.NotaDeCataBodega, varietales);                               
            }

            //MostrarLosVinosDeUnaBodega(bodegaSeleccionada);
            
        }

        public Maridaje BuscarMaridaje(string nombreMaridaje)
        {
            foreach (var maridaje in maridajes) //El metodo BuscarMaridaje itera sobre la lista de maridajes que tenemos declarada y a cada maaridaje de la lista se le pregunta 
                                                // si es el maridaje pasado por parametro(SERIA EL NOMBRE DEL MARIDAJE QUE VIENE EN EL JASON)
            {
                if (maridaje.SosMaridaje(nombreMaridaje)) 
                {
                    return maridaje;            //Nos retorna el objeto Maridaje
                }
            }
            return null; 
        }

        public TipoUva BuscarTipoUva(string nombreTipoUva)
        {
            foreach (var tipoUva in tiposDeUva) //El metodo BuscarTipoUva itera sobre la lista de tiposDeUva que tenemos declarada y a cada tipoDeUva de la lista se le pregunta 
                                                // si es el tipoDeUva pasado por parametro(SERIA EL NOMBRE DEL MARIDAJE QUE VIENE EN EL JASON)
            {
                if (tipoUva.SosTipoUva(nombreTipoUva))
                {
                    return tipoUva;             //Nos retorna el objeto TipoUva
                }
            }
            return null;
        }


        //Este metodo deberia ir en ActualizarCaracteristicasVinosExistentes() para 
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
            mensaje.AppendLine(); 
            MessageBox.Show(mensaje.ToString(), "Vinos de la Bodega");
        }

        public List<Enofilo> buscarSeguidoresDeBodega(Bodega bodega, List<Enofilo> listaEnofilos)
        {
            List<Enofilo> seguidores = new List<Enofilo>();

            foreach (var enofilo in listaEnofilos) // Recorremos todos los enofilos que tenemos
            {
                if (enofilo.SeguisABodega(bodega)) //le Preguntamos a cada uno si sigue a la bodega seleccionada, si la sigue agregamos 
                                                   // el enofilo a la lista seguidores
                {
                    //enofilo.GetNombreUsuario();
                    seguidores.Add(enofilo);
                }
            }

            return seguidores;
        }
        public (List<Vino> vinosParaActualizar, List<Vino> vinosParaCrear) determinarVinosAActualizar(Bodega bodega, List<Vino> listaActualizacionesVinos)
        {
            // De la lista de Vinos de la API filtramos los que son para actualizar y los que se deben crear
            List<Vino> vinosParaActualizar = new List<Vino>();
            List<Vino> vinosParaCrear = new List<Vino>();

            // Aca tenemos en cuenta la alternativa 3
            if (listaActualizacionesVinos == null || listaActualizacionesVinos.Count == 0)
            {
                MessageBox.Show("El Sistema externo de la Bodega "+ bodega.Nombre + " no da respuesta","ERROR");
                
                return (vinosParaActualizar, vinosParaCrear);
            }

            
            foreach (var vino in listaActualizacionesVinos) // Iteramos sobre cada Objeto Vino de la lista que viene de la API
            {
                if (bodega.tenesEsteVino(vino)) //Si la bodega ya tiene ese vino es para actualizar
                {
                    vinosParaActualizar.Add(vino);
                }
                else                            // si la bodega no tiene el vino es para crear
                {
                    vinosParaCrear.Add(vino);
                }
            }

            return (vinosParaActualizar, vinosParaCrear);
        }

        public void crearVino(Bodega bodega, List<Maridaje> maridajes, string añada, string fechaActualizacion, string nombre, int precioARS, int notaDeCataBodega, List<Varietal> varietales)
        {
            bodega.crearVino(maridajes, añada, fechaActualizacion, nombre, precioARS, notaDeCataBodega, varietales);
        }


        public void MostrarLosVinosDeUnaBodega(Bodega bodega)
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

        public List<Vino> obtenerActVinosBodega(InterfazAPIBodega API, Bodega bodegaSelec)
        {
            return API.obtenerActVinosBodega(bodegaSelec);
        }


    }

}
