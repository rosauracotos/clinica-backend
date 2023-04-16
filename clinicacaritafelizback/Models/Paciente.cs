using System;
using System.Collections.Generic;

namespace clinicacaritafelizback.Models
{
    public partial class Paciente
    {
        public Paciente()
        {
            Cita = new HashSet<Cita>();
        }

        public int IdPaciente { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Sexo { get; set; }
        public int? Celular { get; set; }
        public int Dni { get; set; }

        public virtual ICollection<Cita> Cita { get; set; }
    }
}
