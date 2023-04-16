using System;
using System.Collections.Generic;

namespace clinicacaritafelizback.Models
{
    public partial class Cita
    {
        public Cita()
        {
            Comprobante = new HashSet<Comprobante>();
        }

        public int IdCita { get; set; }
        public int? IdPaciente { get; set; }
        public int? IdMedico { get; set; }
        public DateTime FechaHora { get; set; }
        public int? IdAseguradora { get; set; }
        public int? NumeroPoliza { get; set; }
        public int? IdUsuario { get; set; }

        public virtual Aseguradora IdAseguradoraNavigation { get; set; }
        public virtual Medico IdMedicoNavigation { get; set; }
        public virtual Paciente IdPacienteNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
        public virtual ICollection<Comprobante> Comprobante { get; set; }
    }
}
