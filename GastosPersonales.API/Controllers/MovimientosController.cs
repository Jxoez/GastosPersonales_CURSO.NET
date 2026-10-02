using GastosPersonales.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

[Route("api/[controller]")]
[ApiController]
public class MovimientosController : ControllerBase
{
    private readonly GastosPersonalesAPIContext _context;
    public MovimientosController(GastosPersonalesAPIContext context)
    {
        _context = context;
    }

    // Metodo interno para obtener el usuario actual
    private int GetUsuarioActual()
    {
        return int.Parse(
            User.FindFirst(ClaimTypes.NameIdentifier)!.Value
        );
    }

    // GET: api/Movimiento
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Movimiento>>> GetMovimiento()
    {
        var movimientos = await _context.Movimiento.
            Include(c => c.Usuario).
            Include(b => b.Categoria).
            ToListAsync();

        return movimientos;
    }

    // GET: api/Movimiento/5
    [HttpGet("{idmovimiento}")]
    public async Task<ActionResult<Movimiento>> GetMovimiento(int idmovimiento)
    {
        var movimiento = await _context.Movimiento.
            Include(c => c.Usuario).
            Include(b => b.Categoria).
            FirstOrDefaultAsync(c => c.idMovimiento == idmovimiento);

        if (movimiento == null)
        {
            return NotFound();
        }

        return movimiento;
    }

    // PUT: api/Movimiento/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{idmovimiento}")]
    public async Task<IActionResult> PutMovimiento(int? idmovimiento, Movimiento movimiento)
    {
        if (idmovimiento != movimiento.idMovimiento)
        {
            return BadRequest();
        }

        _context.Entry(movimiento).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!MovimientoExists(idmovimiento))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Movimiento
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Movimiento>> PostMovimiento(Movimiento movimiento)
    {
        _context.Movimiento.Add(movimiento);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetMovimiento", new { idmovimiento = movimiento.idMovimiento }, movimiento);
    }

    // DELETE: api/Movimiento/5
    [HttpDelete("{idmovimiento}")]
    public async Task<IActionResult> DeleteMovimiento(int? idmovimiento)
    {
        var movimiento = await _context.Movimiento.FindAsync(idmovimiento);
        if (movimiento == null)
        {
            return NotFound();
        }

        _context.Movimiento.Remove(movimiento);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool MovimientoExists(int? idmovimiento)
    {
        return _context.Movimiento.Any(e => e.idMovimiento == idmovimiento);
    }
}
