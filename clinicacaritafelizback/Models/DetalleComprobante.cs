using System;
using System.Collections.Generic;

namespace clinicacaritafelizback.Models
{
    public partial class DetalleComprobante
    {
        public int IdDetalle { get; set; }
        public int? IdComprobante { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Igv { get; set; }
        public decimal ValorTotal { get; set; }
        public int? IdTarifa { get; set; }

        public virtual Comprobante IdComprobanteNavigation { get; set; }
        public virtual Tarifa IdTarifaNavigation { get; set; }
    }
}
