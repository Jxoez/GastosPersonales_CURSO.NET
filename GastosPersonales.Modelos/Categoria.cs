using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastosPersonales.Modelos
{
    [Table("Categorias") ]
    public class Categoria
    {
        [Key]
        [Column("id_categoria")]
        public int idCategoria { get; set; }

        [Column(TypeName ="varchar(50)")]
        [Required]
        public string nombre { get; set; }

        [Column(TypeName = "varchar(10)")]
        [Required]
        public string tipo { get; set; }

        // Relaciones
        public List<Presupuesto>? Presupuestos { get; set; } = new List<Presupuesto>();
        public List<Movimiento>? Movimientos { get; set; } = new List<Movimiento>();
    }
}
