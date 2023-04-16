using System;
using System.Collections.Generic;

namespace clinicacaritafelizback.Models
{
    public partial class TipoDocumento
    {
        public TipoDocumento()
        {
            Comprobante = new HashSet<Comprobante>();
        }

        public int IdTipoDocumento { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Comprobante> Comprobante { get; set; }
    }
}
