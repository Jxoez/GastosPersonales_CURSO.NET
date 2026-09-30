using Microsoft.EntityFrameworkCore;

public class GastosPersonalesAPIContext(DbContextOptions<GastosPersonalesAPIContext> options) : DbContext(options)
{
    public DbSet<GastosPersonales.Modelos.Usuario> Usuario { get; set; } = default!;
    public DbSet<GastosPersonales.Modelos.Categoria> Categoria { get; set; } = default!;
    public DbSet<GastosPersonales.Modelos.Movimiento> Movimiento { get; set; } = default!;
    public DbSet<GastosPersonales.Modelos.Presupuesto> Presupuesto { get; set; } = default!;
}
