using System;

namespace Mejora_NeptunoAPP.Models
{
    public class DetallePedido
    {
        public int PedidoID { get; set; }
        public int ProductoID { get; set; }
        public decimal PrecioUnidad { get; set; }
        public short Cantidad { get; set; }
        public decimal Descuento { get; set; }
        
        // Joined field
        public DateTime FechaPedido { get; set; }
    }
}
