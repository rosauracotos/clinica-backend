using System;
using System.Collections.Generic;

namespace clinicacaritafelizback.Models
{
    public partial class Aseguradora
    {
        public Aseguradora()
        {
            Cita = new HashSet<Cita>();
        }

        public int IdAseguradora { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Cita> Cita { get; set; }
    }
}
