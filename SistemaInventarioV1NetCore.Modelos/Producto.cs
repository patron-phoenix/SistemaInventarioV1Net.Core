using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventarioV1NetCore.Modelos
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "El número de serie es obligatorio")]
        [MaxLength(60, ErrorMessage = "El número de serie  debe ser maximo de 60 caracteres")]
        public string NumeroSerie { get; set; }


        [Required(ErrorMessage = "La descripción es obligatorio")]
        [MaxLength(100, ErrorMessage = "La descripción debe ser maximo de 100 caracteres")]
        public string Descripcion { get; set; }


        [Required(ErrorMessage = "El precio es obligatorio")]
        public double Precio { get; set; }


        [Required(ErrorMessage = "El costo es obligatorio")]
        public double Costo { get; set; }

        public string ImagenUrl { get; set; }


        [Required(ErrorMessage = "El estado es obligatorio")]
        public bool Estado { get; set; }



        [Required(ErrorMessage = "La Categoria es obligatorio")]
        public int CategoriaId { get; set; }

        [ForeignKey(nameof(CategoriaId))]
        public Categoria Categoria { get; set; }


        [Required(ErrorMessage = "La Marca es obligatorio")]
        public int MarcaId { get; set; }

        [ForeignKey(nameof(MarcaId))]
        public Marca Marca { get; set; }


        public int? PadreId { get; set; }


        public virtual Producto Padre { get; set; }

    }
}
