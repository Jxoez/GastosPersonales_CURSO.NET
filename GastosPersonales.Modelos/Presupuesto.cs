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
        public int idPresupuesto;

        [Column("monto_limite",TypeName = "numeric(10,2)")]
        [Required]
        public double montoLimite;

        [Required]
        public int mes;

        [Required]
        public int anio;

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
