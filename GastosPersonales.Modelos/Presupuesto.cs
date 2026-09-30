using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastosPersonales.Modelos
{
    [Table("Presupuestos")]
    public class Presupuesto
    {
        [Key]
        [Column("id_presupuesto")]
        public int idPresupuesto { get; set; }

        [Column("monto_limite",TypeName = "numeric(10,2)")]
        [Required]
        public double montoLimite { get; set; }

        [Required]
        public int mes { get; set; }

        [Required]
        public int anio { get; set; }

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
