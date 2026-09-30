using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastosPersonales.Modelos
{
    public class Movimiento
    {
        [Key]
        [Column("id_movimiento")]
        public int idMovimiento;

        [Column(TypeName = "varchar(150)")]
        [Required]
        public string descripcion;

        [Column(TypeName = "numeric(10,2)")]
        [Required]
        public double monto;

        [Column(TypeName = "date")]
        [Required]
        public DateOnly fecha;

        [Column(TypeName = "varchar(10)")]
        [Required]
        public string tipo;

        [ForeignKey("Usuario")]
        [Required]
        public int idUsuario { get; set; }

        [ForeignKey("Categoria")]
        [Required]
        public int idCategoria { get; set; }

        // Objetos de navegacion
        public Usuario? Usuario { get; set; }
        public Categoria? Categoria { get; set; }

    }
}
