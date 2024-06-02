using PPAI2024.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAI2024.Gestor_e_interfaces
{
    public class InterfazAPIBodega
    {
        public List<Vino> ObtenerActualizacionesBodega(Bodega bodega)
        {
            // Simulamos la obtención de datos de la API
            // Supongamos que la API devuelve una lista de vinos  a actualizar
            DateTime fechaActual = DateTime.Now;
            List<Vino> listaParaProbarAlternativa = new List<Vino> { };
            List<Vino> actualizaciones = new List<Vino>
            
            {
        new Vino(
            new List<Maridaje> { new Maridaje("Carnes Rojas", "Ideal para carnes rojas a la parrilla", new TipoUva("Malbec", "Uva tinta")) },
            bodega,
            "2018",
           fechaActual.ToString("yyyy-MM-dd"),
            "Malbec Reserva",
            1300, // Precio
            95,  // Nota de Cata
            new List<Varietal> { new Varietal("Malbec", 100, new TipoUva("Malbec", "Uva tinta")) }
        ),
        new Vino(
            new List<Maridaje> { new Maridaje("Pescados", "Perfecto para pescados y mariscos", new TipoUva("Torrontés", "Uva blanca")) },
            bodega,
            "2020",
           fechaActual.ToString("yyyy-MM-dd"),
            "Torrontés Clásico",
            180, // Precio
            88,  // Nota de Cata
            new List<Varietal> { new Varietal("Torrontés", 100, new TipoUva("Torrontés", "Uva blanca")) }
        ),
        new Vino(
            new List<Maridaje> { new Maridaje("Pastas", "Ideal para acompañar pastas y salsas rojas", new TipoUva("Bonarda", "Uva tinta")) },
            bodega,
            "2019",
            fechaActual.ToString("yyyy-MM-dd"),
            "Bonarda Clásica",
            220, // Precio
            87,  // Nota de Cata
            new List<Varietal> { new Varietal("Bonarda", 100, new TipoUva("Bonarda", "Uva tinta")) }
        ),
        new Vino(
            new List<Maridaje> { new Maridaje("Carnes Blancas", "Perfecto para carnes blancas y ensaladas", new TipoUva("Chardonnay", "Uva blanca")) },
            bodega,
            "2021",
            fechaActual.ToString("yyyy-MM-dd"),
            "Chardonnay Premium",
            280, // Precio
            89,  // Nota de Cata
            new List<Varietal> { new Varietal("Chardonnay", 100, new TipoUva("Chardonnay", "Uva blanca")) }
        ),
        new Vino(
            new List<Maridaje> { new Maridaje("Quesos", "Ideal para tablas de quesos y aperitivos", new TipoUva("Malbec", "Uva tinta")) },
            bodega,
            "2022",
            fechaActual.ToString("yyyy-MM-dd"),
            "Malbec Joven",
            200, // Precio
            86,  // Nota de Cata
            new List<Varietal> { new Varietal("Malbec", 100, new TipoUva("Malbec", "Uva tinta")) }
        ),
        new Vino(
            new List<Maridaje> { new Maridaje("Carnes Rojas", "Ideal para carnes rojas a la parrilla", new TipoUva("Cabernet Sauvignon", "Uva tinta")) },
            bodega,
            "2019",
            fechaActual.ToString("yyyy-MM-dd"),
            "Cabernet Sauvignon Gran Reserva",
            300, // Precio
            91,  // Nota de Cata
            new List<Varietal> { new Varietal("Cabernet Sauvignon", 100, new TipoUva("Cabernet Sauvignon", "Uva tinta")) }
        )
    };

            return listaParaProbarAlternativa;
        }



    }
}
