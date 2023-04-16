using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using clinicacaritafelizback.Models;



namespace clinicacaritafelizback.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private readonly CLINICAContext BD;

        public PacienteController(CLINICAContext context)
        {
            BD = context;
        }

        //GET. /api/paciente/2
        [HttpGet("{id}", Name = "PacienteCreado")]
        public IActionResult Paciente(int id)
        {
            Paciente paciente = new Paciente();

            paciente = (from u in BD.Paciente
                      where u.IdPaciente == id
                      select new Paciente
                      {
                          IdPaciente = u.IdPaciente,
                          Nombre = u.Nombre,
                          Apellido = u.Apellido,
                          Direccion = u.Direccion,
                          FechaNacimiento = u.FechaNacimiento,
                          Sexo = u.Sexo,
                          Celular = u.Celular,
                          Dni = u.Dni,
                      }).ToList().FirstOrDefault();

            return Ok(paciente);
        }

        //POST. /api/paciente
        [HttpPost()]
        public IActionResult CrearPaciente([FromBody] Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                //guardamos el paciente en la BD
                BD.Paciente.Add(paciente);
                BD.SaveChanges();

                //devolvemos el paciente recientemente creado
                return new CreatedAtRouteResult("PacienteCreado", new { id = paciente.IdPaciente }, paciente);
            }

            return BadRequest(ModelState);
        }

        //PUT. /api/paciente/5
        [HttpPut("{id}")]
        public IActionResult ModificarPaciente([FromBody] Paciente paciente, int id)
        {
            if (paciente.IdPaciente != id)
            {
                return BadRequest();
            }

            BD.Entry(paciente).State = EntityState.Modified;
            BD.SaveChanges();

            return Ok();
        }


        //DELETE. /api/paciente/5
        [HttpDelete("{id}")]
        public IActionResult EliminarPaciente(int id)
        {
            var paciente = BD.Paciente.FirstOrDefault(u => u.IdPaciente == id);

            if (paciente == null)
            {
                return NotFound();
            }

            BD.Paciente.Remove(paciente);
            BD.SaveChanges();

            return Ok(paciente);
        }
    }
}
