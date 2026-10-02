using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GastosPersonales.Modelos;

[Route("api/[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly GastosPersonalesAPIContext _context;
    public CategoriasController(GastosPersonalesAPIContext context)
    {
        _context = context;
    }

    // GET: api/Categoria
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Categoria>>> GetCategoria()
    {
        var categorias = await _context.Categoria.
            Include(c => c.Presupuestos).
            Include(c => c.Movimientos)
            .ToListAsync();
        return categorias;
    }

    // GET: api/Categoria/5
    [HttpGet("{idcategoria}")]
    public async Task<ActionResult<Categoria>> GetCategoria(int idcategoria)
    {
        var categoria = await _context.Categoria.
            Include(c => c.Presupuestos).
            Include(c => c.Movimientos)
            .FirstOrDefaultAsync(c => c.idCategoria == idcategoria);

        if (categoria == null)
        {
            return NotFound();
        }

        return categoria;
    }

    // PUT: api/Categoria/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{idcategoria}")]
    public async Task<IActionResult> PutCategoria(int? idcategoria, Categoria categoria)
    {
        if (idcategoria != categoria.idCategoria)
        {
            return BadRequest();
        }

        _context.Entry(categoria).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CategoriaExists(idcategoria))
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

    // POST: api/Categoria
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Categoria>> PostCategoria(Categoria categoria)
    {
        _context.Categoria.Add(categoria);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetCategoria", new { idcategoria = categoria.idCategoria }, categoria);
    }

    // DELETE: api/Categoria/5
    [HttpDelete("{idcategoria}")]
    public async Task<IActionResult> DeleteCategoria(int? idcategoria)
    {
        var categoria = await _context.Categoria.FindAsync(idcategoria);
        if (categoria == null)
        {
            return NotFound();
        }

        _context.Categoria.Remove(categoria);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool CategoriaExists(int? idcategoria)
    {
        return _context.Categoria.Any(e => e.idCategoria == idcategoria);
    }
}
