using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using clinicacaritafelizback.Models;

namespace clinicacaritafelizback.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicoController : ControllerBase
    {

        private readonly CLINICAContext BD;

        public MedicoController(CLINICAContext context)
        {
            BD = context;
        }

        //GET. /api/medico/2
        [HttpGet("{id}", Name = "MedicoCreado")]
        public IActionResult Medico(int id)
        {
            Medico medico = new Medico();

            medico = (from u in BD.Medico
                       where u.IdMedico == id
                       select new Medico
                       {
                           IdMedico = u.IdMedico,
                           Nombre = u.Nombre,
                           Apellido = u.Apellido,
                           Direccion = u.Direccion,
                           FechaNacimiento = u.FechaNacimiento,
                           Sexo = u.Sexo,
                           Celular = u.Celular,
                           Dni = u.Dni,
                           Cmp = u.Cmp,
                           IdEspecialidad = u.IdEspecialidad,
                           IdEspecialidadNavigation = ObtenerEspecialidad(u.IdEspecialidad.GetValueOrDefault())
                       }).ToList().FirstOrDefault();

            return Ok(medico);
        }

        private Especialidad ObtenerEspecialidad(int IdEspecialidad)
        {
            Especialidad especialidad = new Especialidad();

            using (CLINICAContext BD1 = new CLINICAContext())
            {
                especialidad = (from r in BD1.Especialidad
                       where r.IdEspecialidad == IdEspecialidad
                                select r
                   ).ToList().First();
            }

            return especialidad;
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
