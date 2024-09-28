using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaInventarioV1NetCore.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventarioV1NetCore.AccesoDatos.Repositorio.IRepositorio
{
    public interface IProductoRepositorio : IRepositorio<Producto>
    {
        void Actualizar(Producto producto);

        IEnumerable<SelectListItem> ObtenerTodosDropdownList(string obj);
    }
}
