using System;
using System.Collections.Generic;

namespace clinicacaritafelizback.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Cita = new HashSet<Cita>();
        }

        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Sexo { get; set; }
        public int? Celular { get; set; }
        public int Dni { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int? IdRol { get; set; }

        public virtual Rol IdRolNavigation { get; set; }
        public virtual ICollection<Cita> Cita { get; set; }
    }
}
