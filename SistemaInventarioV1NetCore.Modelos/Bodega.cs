using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventarioV1NetCore.Modelos
{
    public class Bodega
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="El nombre de la bodega es obligatorio")]
        [MaxLength(60,ErrorMessage ="El nombre debe ser maximo de 60 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripción es obligatorio")]
        [MaxLength(100, ErrorMessage = "La descripción debe ser maximo de 100 caracteres")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "La estado es obligatorio")]
        public bool Estado { get; set; }
    }
}
