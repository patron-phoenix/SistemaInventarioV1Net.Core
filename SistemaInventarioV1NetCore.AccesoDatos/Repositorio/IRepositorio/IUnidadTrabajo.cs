﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventarioV1NetCore.AccesoDatos.Repositorio.IRepositorio
{
    public interface IUnidadTrabajo:IDisposable
    {
        IBodegaRepositorio Bodega {  get; }
        ICategoriaRepositorio Categoria { get; }
        Task Guardar();
    }
}
