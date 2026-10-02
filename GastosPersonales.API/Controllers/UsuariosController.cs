using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GastosPersonales.Modelos;


[Route("api/[controller]")]
[ApiController]
public class UsuariosController : ControllerBase
{
    private readonly GastosPersonalesAPIContext _context;
    public UsuariosController(GastosPersonalesAPIContext context)
    {
        _context = context;
    }

    // GET: api/Usuario
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuario()
    {
        var usuarios = await _context.Usuario.
            Include(u => u.Presupuestos).
            Include(u => u.Movimientos)
            .ToListAsync();
        return usuarios;
    }

    // GET: api/Usuario/5
    [HttpGet("{idusuario}")]
    public async Task<ActionResult<Usuario>> GetUsuario(int idusuario)
    {
        var usuario = await _context.Usuario.
            Include(u => u.Presupuestos).
            Include(u => u.Movimientos)
            .FirstOrDefaultAsync(u => u.idUsuario == idusuario);

        if (usuario == null)
        {
            return NotFound();
        }

        return usuario;
    }

    // PUT: api/Usuario/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{idusuario}")]
    public async Task<IActionResult> PutUsuario(int? idusuario, Usuario usuario)
    {
        if (idusuario != usuario.idUsuario)
        {
            return BadRequest();
        }

        var usuarioExistente = await _context.Usuario.FindAsync(idusuario);
        if (usuarioExistente == null)
        {
            return NotFound();
        }

        usuarioExistente.nombre = usuario.nombre;
        usuarioExistente.apellido = usuario.apellido;
        usuarioExistente.email = usuario.email;

        if (!string.IsNullOrEmpty(usuario.password))
        {
            usuarioExistente.password = BCrypt.Net.BCrypt.HashPassword(usuario.password);
        }

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // POST: api/Usuario
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
    {
        var existe = await _context.Usuario.AnyAsync(
            u => u.email.ToLower() == usuario.email.ToLower()
        );

        if (existe)
        {
            return Conflict("El correo electrónico ya está registrado.");
        }

        usuario.password = BCrypt.Net.BCrypt.HashPassword(usuario.password);

        _context.Usuario.Add(usuario);

        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetUsuario),
            new { idusuario = usuario.idUsuario },
            usuario
        );
    }

    // DELETE: api/Usuario/5
    [HttpDelete("{idusuario}")]
    public async Task<IActionResult> DeleteUsuario(int? idusuario)
    {
        var usuario = await _context.Usuario.FindAsync(idusuario);
        if (usuario == null)
        {
            return NotFound();
        }

        _context.Usuario.Remove(usuario);
        await _context.SaveChangesAsync();

        return NoContent();
    }

}
