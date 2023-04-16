
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using clinicacaritafelizback.Models;

namespace clinicacaritafelizback.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly CLINICAContext BD;

        public UsuarioController(CLINICAContext context)
        {
            BD = context;
        }


        public bool Login(string username, string password)
        {
            using (var context = new CLINICAContext())
            {
                var user = context.Usuario.FirstOrDefault(u => u.Login == username && u.Password == password);

                if (user != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        //GET. /api/usuario/2
        [HttpGet("{id}", Name = "UsuarioCreado")]
        public IActionResult Usuario(int id)
        {
            Usuario usuario = new Usuario();

            usuario=(from u in BD.Usuario
                     where u.IdUsuario==id
                     select new Usuario
                     {
                         IdUsuario = u.IdUsuario,
                         Nombre=u.Nombre,
                         Apellido= u.Apellido,
                         Sexo=u.Sexo,
                         Celular=u.Celular,
                         Dni=u.Dni,
                         Login=u.Login,
                         Password=u.Password,
                         IdRol=u.IdRol,
                         IdRolNavigation= ObtenerRol(u.IdRol.GetValueOrDefault())
                     }).ToList().FirstOrDefault();

            return Ok(usuario);
        }


        private Rol ObtenerRol(int uIdRol)
        {
            Rol rol = new Rol();

            using (CLINICAContext BD1 = new CLINICAContext())
            {
                rol = (from r in BD1.Rol
                       where r.IdRol == uIdRol
                       select r
                   ).ToList().First();
            }

            return rol;
        }

        //POST. /api/usuario
        [HttpPost()]
        public IActionResult CrearUsuario([FromBody] Usuario pUsuario)
        {
            if (ModelState.IsValid)
            {
                //guardamos el usuario en la BD
                BD.Usuario.Add(pUsuario);
                BD.SaveChanges();

                //devolvemos el usurio recientemente creado
                return new CreatedAtRouteResult("UsuarioCreado", new { id = pUsuario.IdUsuario }, pUsuario);
            }

            return BadRequest(ModelState);
        }

        //PUT. /api/usuario/5
        [HttpPut("{id}")]
        public IActionResult ModificarUsuario([FromBody] Usuario pUsuario, int id)
        {
            if (pUsuario.IdUsuario != id)
            {
                return BadRequest();
            }

            BD.Entry(pUsuario).State = EntityState.Modified;
            BD.SaveChanges();

            return Ok();
        }


        //DELETE. /api/usuario/5
        [HttpDelete("{id}")]
        public IActionResult EliminarUsuario(int id)
        {
            var usuario = BD.Usuario.FirstOrDefault(u => u.IdUsuario == id);

            if (usuario == null)
            {
                return NotFound();
            }

            BD.Usuario.Remove(usuario);
            BD.SaveChanges();

            return Ok(usuario);
        }








    }
}
