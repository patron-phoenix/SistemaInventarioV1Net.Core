using SistemaInventarioV1NetCore.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventarioV1NetCore.AccesoDatos.Repositorio.IRepositorio
{
    public interface IBodegaRepositorio: IRepositorio<Bodega>
    {
        void Actualizar(Bodega bodega);
    }
}
