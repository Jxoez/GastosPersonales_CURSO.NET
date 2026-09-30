using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastosPersonales.Modelos
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        [Column("id_usuario")]
        public int idUsuario;
        
        [Column(TypeName = "varchar(50)")]
        [Required]
        public string nombre;
        
        [Column(TypeName = "varchar(50)")]
        [Required]
        public string apellido;

        [Column(TypeName = "varchar(10)")]
        [Required]
        public string email;

        [Column(TypeName = "varchar(50)")]
        [Required]
        public string password;

        // Relaciones
        public List<Presupuesto>? Presupuestos { get; set; } = new List<Presupuesto>();
        public List<Movimiento>? Movimientos { get; set; } = new List<Movimiento>();


    }
}
