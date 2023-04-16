using System;
using System.Collections.Generic;

namespace clinicacaritafelizback.Models
{
    public partial class Comprobante
    {
        public Comprobante()
        {
            DetalleComprobante = new HashSet<DetalleComprobante>();
        }

        public int IdComprobante { get; set; }
        public int IdTipoDocumento { get; set; }
        public string Serie { get; set; }
        public string Numero { get; set; }
        public DateTime FechaEmision { get; set; }
        public decimal ValorTotal { get; set; }
        public int IdCita { get; set; }

        public virtual Cita IdCitaNavigation { get; set; }
        public virtual TipoDocumento IdTipoDocumentoNavigation { get; set; }
        public virtual ICollection<DetalleComprobante> DetalleComprobante { get; set; }
    }
}
