using System;
using System.Collections.Generic;

namespace clinicacaritafelizback.Models
{
    public partial class Especialidad
    {
        public Especialidad()
        {
            Medico = new HashSet<Medico>();
        }

        public int IdEspecialidad { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Medico> Medico { get; set; }
    }
}
