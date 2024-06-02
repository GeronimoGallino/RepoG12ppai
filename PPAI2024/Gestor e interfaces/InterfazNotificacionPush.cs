using PPAI2024.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PPAI2024.Gestor_e_interfaces
{
    public class InterfazNotificacionPush
    {


        public void NotificarNovedadVinoParaBodega(List<Enofilo> enofilos, Bodega bodega)
        {
            foreach (var enofilo in enofilos)
            {
                if (enofilo.SeguisABodega(bodega))
                {
                    // Enviar la notificación al enófilo
                    MessageBox.Show($"Notificación enviada a {enofilo.Nombre} {enofilo.Apellido}: La bodega {bodega.Nombre} ha actualizado su información.");
                }
    }   }   }
}
