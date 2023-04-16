using System;
using System.Collections.Generic;

namespace clinicacaritafelizback.Models
{
    public partial class Tarifa
    {
        public Tarifa()
        {
            DetalleComprobante = new HashSet<DetalleComprobante>();
        }

        public int IdTarifa { get; set; }
        public string Nombre { get; set; }
        public decimal PrecioParticular { get; set; }
        public decimal PrecioAsegurado { get; set; }

        public virtual ICollection<DetalleComprobante> DetalleComprobante { get; set; }
    }
}
