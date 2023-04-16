
using Microsoft.AspNetCore.Mvc;
using clinicacaritafelizback.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;



namespace clinicacaritafelizback.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly CLINICAContext BD;

        public RolController(CLINICAContext context)
        {
            BD = context;
        }

        //GET. /api/rol
        [HttpGet]
        public IEnumerable<Rol> ListaDeRol()
        {
            return BD.Rol.ToList();
        }

        //GET. /api/rol/2
        [HttpGet("{id}", Name = "RolCreado")]
        public IActionResult DevolverRol(int id)
        {
            var rol = BD.Rol.FirstOrDefault(u => u.IdRol == id);

            if (rol == null)
            {
                return NotFound();
            }

            return Ok(rol);
        }


        //POST. /api/rol
        [HttpPost()]
        public IActionResult CrearRol([FromBody] Rol rol)
        {
            if (ModelState.IsValid)
            {
                //guardamos el usuario en la BD
                BD.Rol.Add(rol);
                BD.SaveChanges();

                //devolvemos el usurio recientemente creado
                return new CreatedAtRouteResult("Rol Creado", new { id = rol.IdRol }, rol);
            }

            return BadRequest(ModelState);
        }

        //PUT. /api/rol/5
        [HttpPut("{id}")]
        public IActionResult ModificarRol([FromBody] Rol rol, int id)
        {
            if (rol.IdRol != id)
            {
                return BadRequest();
            }

            BD.Entry(rol).State = EntityState.Modified;
            BD.SaveChanges();

            return Ok();
        }

        //DELETE. /api/rol/5
        [HttpDelete("{id}")]
        public IActionResult EliminarRol(int id)
        {
            var rol = BD.Usuario.FirstOrDefault(u => u.IdRol == id);

            if (rol == null)
            {
                return NotFound();
            }

            BD.Usuario.Remove(rol);
            BD.SaveChanges();

            return Ok(rol);
        }
    }
}
