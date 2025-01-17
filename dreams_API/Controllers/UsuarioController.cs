using Microsoft.AspNetCore.Mvc;
using dreams_API.Data;
using dreams_API.Models;
using Microsoft.AspNetCore.Authorization;


namespace dreams_API.Controllers
{
    [Route("api/[controller]"), Authorize(Roles="administrador")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private DreamsContext _context;
        public UsuarioController(DreamsContext context)
        {
            //construtor
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Usuario>> GetAll()
        {
            if (_context.Usuario is not null)
            {
                return _context.Usuario.ToList();
            }
            else
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no acesso ao banco de dados.");
            }
        }

        [HttpGet("{UsuarioId}")]
        public ActionResult<List<Usuario>> Get(int UsuarioId)
        {
            try
            {
                var result = _context.Usuario.Find(UsuarioId);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no acesso ao banco de dados.");
            }
        }

        [HttpPost]
        public async Task<ActionResult> post(Usuario model)
        {
            try
            {
                _context.Usuario.Add(model);
                if (await _context.SaveChangesAsync() == 1)
                {
                    //return Ok();
                    return Created($"/api/usuario/{model.username}", model);
                }
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no acesso ao banco de dados");
            }
            return BadRequest();

        }

        /*[HttpPut("{UsuarioId}")]
        //[Authorize(Roles = "administrador")]
        public async Task<ActionResult> put(int UsuarioId, Usuario dadosUsuarioAlt)
        {
            try
            {
                //verifica se existe aluno a ser alterado
                var result = await _context.Usuario.FindAsync(UsuarioId);
                if (UsuarioId != null)
                {
                    //método do EF
                    return BadRequest();
                }
                result.Usuarioname = dadosUsuarioAlt.Usuarioname;
                result.senha = dadosUsuarioAlt.senha;
                result.cargo = dadosUsuarioAlt.cargo;
                await _context.SaveChangesAsync();
                return Created($"/api/usuario/{dadosUsuarioAlt.Usuarioname}", dadosUsuarioAlt);
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no acesso ao banco de dados.");
            }
        }

        [HttpDelete("{UsuarioId}")]
        //[Authorize(Roles="administrador")]
        public async Task<ActionResult> delete(int UsuarioId)
        {
            try
            {
                //verifica se existe aluno a ser excluido
                var usuario = await _context.Usuario.FindAsync(UsuarioId);
                if (usuario == null)
                {
                    //método do EF
                    return NotFound();
                }
                _context.Remove(usuario);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no acesso ao banco de dados.");
            }
        }*/
    }
}