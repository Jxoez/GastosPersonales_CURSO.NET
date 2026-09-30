using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GastosPersonales.Modelos;

[Route("api/[controller]")]
[ApiController]
public class PresupuestosController : ControllerBase
{
    private readonly GastosPersonalesAPIContext _context;
    public PresupuestosController(GastosPersonalesAPIContext context)
    {
        _context = context;
    }

    // GET: api/Presupuesto
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Presupuesto>>> GetPresupuesto()
    {
        return await _context.Presupuesto.ToListAsync();
    }

    // GET: api/Presupuesto/5
    [HttpGet("{idpresupuesto}")]
    public async Task<ActionResult<Presupuesto>> GetPresupuesto(int idpresupuesto)
    {
        var presupuesto = await _context.Presupuesto.FindAsync(idpresupuesto);

        if (presupuesto == null)
        {
            return NotFound();
        }

        return presupuesto;
    }

    // PUT: api/Presupuesto/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{idpresupuesto}")]
    public async Task<IActionResult> PutPresupuesto(int? idpresupuesto, Presupuesto presupuesto)
    {
        if (idpresupuesto != presupuesto.idPresupuesto)
        {
            return BadRequest();
        }

        _context.Entry(presupuesto).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PresupuestoExists(idpresupuesto))
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

    // POST: api/Presupuesto
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Presupuesto>> PostPresupuesto(Presupuesto presupuesto)
    {
        _context.Presupuesto.Add(presupuesto);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetPresupuesto", new { idpresupuesto = presupuesto.idPresupuesto }, presupuesto);
    }

    // DELETE: api/Presupuesto/5
    [HttpDelete("{idpresupuesto}")]
    public async Task<IActionResult> DeletePresupuesto(int? idpresupuesto)
    {
        var presupuesto = await _context.Presupuesto.FindAsync(idpresupuesto);
        if (presupuesto == null)
        {
            return NotFound();
        }

        _context.Presupuesto.Remove(presupuesto);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool PresupuestoExists(int? idpresupuesto)
    {
        return _context.Presupuesto.Any(e => e.idPresupuesto == idpresupuesto);
    }
}
