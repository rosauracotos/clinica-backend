
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using clinicacaritafelizback.Models;


namespace clinicacaritafelizback.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitaController : ControllerBase
    {
        private readonly CLINICAContext BD;

        public CitaController(CLINICAContext context)
        {
            BD = context;
        }

        //GET. /api/cita/2
        [HttpGet("{id}", Name = "CitaCreado")]
        public IActionResult Cita(int id)
        {
            Cita cita = new Cita();

            cita = (from u in BD.Cita
                      where u.IdCita == id
                      select new Cita
                      {
                          IdCita = u.IdCita,
                          IdPaciente = u.IdPaciente,
                          IdMedico = u.IdMedico,
                          FechaHora = u.FechaHora,
                          IdAseguradora = u.IdAseguradora,
                          NumeroPoliza = u.NumeroPoliza,
                          IdUsuario = u.IdUsuario,
                          IdAseguradoraNavigation = ObtenerAseguradora(u.IdAseguradora.GetValueOrDefault()),
                          IdMedicoNavigation = ObtenerMedico(u.IdMedico.GetValueOrDefault()),
                          IdPacienteNavigation = ObtenerPaciente(u.IdPaciente.GetValueOrDefault()),
                          IdUsuarioNavigation = ObtenerUsuario(u.IdUsuario.GetValueOrDefault())
                      }).ToList().FirstOrDefault();

            return Ok(cita);
        }

        private Aseguradora ObtenerAseguradora(int IdAseguradora)
        {
            Aseguradora aseguradora = new Aseguradora();

            using (CLINICAContext BD1 = new CLINICAContext())
            {
                aseguradora = (from r in BD1.Aseguradora
                                where r.IdAseguradora == IdAseguradora
                                select r
                   ).ToList().First();
            }

            return aseguradora;
        }

        private Medico ObtenerMedico(int IdMedico)
        {
            Medico medico = new Medico();

            using (CLINICAContext BD1 = new CLINICAContext())
            {
                medico = (from r in BD1.Medico
                               where r.IdMedico == IdMedico
                               select r
                   ).ToList().First();
            }

            return medico;
        }

        private Paciente ObtenerPaciente(int IdPaciente)
        {
            Paciente paciente = new Paciente();

            using (CLINICAContext BD1 = new CLINICAContext())
            {
                paciente = (from r in BD1.Paciente
                          where r.IdPaciente == IdPaciente
                          select r
                   ).ToList().First();
            }

            return paciente;
        }

        private Usuario ObtenerUsuario(int IdUsuario)
        {
            Usuario usuario = new Usuario();

            using (CLINICAContext BD1 = new CLINICAContext())
            {
                usuario = (from r in BD1.Usuario
                            where r.IdUsuario == IdUsuario
                            select r
                   ).ToList().First();
            }

            return usuario;
        }






        //POST. /api/medico
        [HttpPost()]
        public IActionResult CrearMedico([FromBody] Medico medico)
        {
            if (ModelState.IsValid)
            {
                //guardamos el medico en la BD
                BD.Medico.Add(medico);
                BD.SaveChanges();

                //devolvemos el medico recientemente creado
                return new CreatedAtRouteResult("MedicoCreado", new { id = medico.IdMedico }, medico);
            }

            return BadRequest(ModelState);
        }

        //PUT. /api/medico/5
        [HttpPut("{id}")]
        public IActionResult ModificarMedico([FromBody] Medico medico, int id)
        {
            if (medico.IdMedico != id)
            {
                return BadRequest();
            }

            BD.Entry(medico).State = EntityState.Modified;
            BD.SaveChanges();

            return Ok();
        }


        //DELETE. /api/medico/5
        [HttpDelete("{id}")]
        public IActionResult EliminarMedico(int id)
        {
            var medico = BD.Medico.FirstOrDefault(u => u.IdMedico == id);

            if (medico == null)
            {
                return NotFound();
            }

            BD.Medico.Remove(medico);
            BD.SaveChanges();

            return Ok(medico);
        }
    }
}
