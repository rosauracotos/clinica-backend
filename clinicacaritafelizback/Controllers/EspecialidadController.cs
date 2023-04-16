using Microsoft.AspNetCore.Mvc;
using clinicacaritafelizback.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace clinicacaritafelizback.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadController : ControllerBase
    {
        private readonly CLINICAContext BD;

        public EspecialidadController(CLINICAContext context)
        {
            BD = context;
        }

        //GET. /api/especialidad
        [HttpGet]
        public IEnumerable<Especialidad> ListaDeEspecialidad()
        {
            return BD.Especialidad.ToList();
        }

        //GET. /api/especialidad/2
        [HttpGet("{id}", Name = "EspecialidadCreado")]
        public IActionResult DevolverEspecialidad(int id)
        {
            var especialidad = BD.Especialidad.FirstOrDefault(u => u.IdEspecialidad == id);

            if (especialidad == null)
            {
                return NotFound();
            }

            return Ok(especialidad);
        }


        //POST. /api/especialidad
        [HttpPost()]
        public IActionResult CrearEspecialidad([FromBody] Especialidad especialidad)
        {
            if (ModelState.IsValid)
            {
                //guardamos el usuario en la BD
                BD.Especialidad.Add(especialidad);
                BD.SaveChanges();

                //devolvemos el usurio recientemente creado
                return new CreatedAtRouteResult("Especialidad Creado", new { id = especialidad.IdEspecialidad }, especialidad);
            }

            return BadRequest(ModelState);
        }

        //PUT. /api/especialidad/5
        [HttpPut("{id}")]
        public IActionResult ModificarEspecialidad([FromBody] Especialidad especialidad, int id)
        {
            if (especialidad.IdEspecialidad != id)
            {
                return BadRequest();
            }

            BD.Entry(especialidad).State = EntityState.Modified;
            BD.SaveChanges();

            return Ok();
        }

        //DELETE. /api/especialidad/5
        [HttpDelete("{id}")]
        public IActionResult EliminarEspecialidad(int id)
        {
            var especialidad = BD.Especialidad.FirstOrDefault(u => u.IdEspecialidad == id);

            if (especialidad == null)
            {
                return NotFound();
            }

            BD.Especialidad.Remove(especialidad);
            BD.SaveChanges();

            return Ok(especialidad);
        }
    }

}
