
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using clinicacaritafelizback.Models;


namespace clinicacaritafelizback.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AseguradoraController : ControllerBase
    {
        private readonly CLINICAContext BD;

        public AseguradoraController(CLINICAContext context)
        {
            BD = context;
        }

        //GET. /api/aseguradora
        [HttpGet]
        public IEnumerable<Aseguradora> ListaDeAseguradora()
        {
            return BD.Aseguradora.ToList();
        }

        //GET. /api/aseguradora/2
        [HttpGet("{id}", Name = "AseguradoraCreado")]
        public IActionResult DevolverAseguradora(int id)
        {
            var aseguradora = BD.Aseguradora.FirstOrDefault(u => u.IdAseguradora == id);

            if (aseguradora == null)
            {
                return NotFound();
            }

            return Ok(aseguradora);
        }


        //POST. /api/aseguradora
        [HttpPost()]
        public IActionResult CrearAseguradora([FromBody] Aseguradora aseguradora)
        {
            if (ModelState.IsValid)
            {
                //guardamos el aseguradora en la BD
                BD.Aseguradora.Add(aseguradora);
                BD.SaveChanges();

                //devolvemos el aseguradora recientemente creado
                return new CreatedAtRouteResult("Aseguradora Creado", new { id = aseguradora.IdAseguradora }, aseguradora);
            }

            return BadRequest(ModelState);
        }

        //PUT. /api/aseguradora/5
        [HttpPut("{id}")]
        public IActionResult ModificarAseguradora([FromBody] Aseguradora aseguradora, int id)
        {
            if (aseguradora.IdAseguradora != id)
            {
                return BadRequest();
            }

            BD.Entry(aseguradora).State = EntityState.Modified;
            BD.SaveChanges();

            return Ok();
        }

        //DELETE. /api/aseguradora/5
        [HttpDelete("{id}")]
        public IActionResult EliminarAseguradora(int id)
        {
            var aseguradora = BD.Aseguradora.FirstOrDefault(u => u.IdAseguradora == id);

            if (aseguradora == null)
            {
                return NotFound();
            }

            BD.Aseguradora.Remove(aseguradora);
            BD.SaveChanges();

            return Ok(aseguradora);
        }
    }
}
