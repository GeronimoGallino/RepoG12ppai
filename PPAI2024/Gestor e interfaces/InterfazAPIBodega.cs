using PPAI2024.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAI2024.Gestor_e_interfaces
{
    internal class InterfazAPIBodega
    {
        public List<Vino> ObtenerActualizacionesBodega(Bodega bodega)
        {
            // Simulamos obtener actualizaciones de la bodega. En un caso real, esto se obtendría de una API REST.
            // Aquí estamos simplemente creando algunos datos de muestra.

            var actualizaciones = new List<Vino>
        {
            new Vino(new List<Maridaje> { new Maridaje("Queso", "Roquefort"), new Maridaje("Carne", "Bife de chorizo") }, bodega, "2021", DateTime.Now.ToString("yyyy-MM-dd"), "Vino A", 1200, 85),
            new Vino(new List<Maridaje> { new Maridaje("Carne", "Cordero"), new Maridaje("Vegetariano", "Ensalada de lentejas") }, bodega, "2020", DateTime.Now.ToString("yyyy-MM-dd"), "Vino B", 1500, 90),
            new Vino(new List<Maridaje> { new Maridaje("Postre", "Tarta de frutas") }, bodega, "2019", DateTime.Now.ToString("yyyy-MM-dd"), "Vino C", 1800, 88)
        };

            return actualizaciones;
        }
    }
}
