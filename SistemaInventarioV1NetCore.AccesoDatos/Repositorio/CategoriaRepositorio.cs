using Microsoft.EntityFrameworkCore.Metadata;
using SistemaInventarioV1NetCore.AccesoDatos.Data;
using SistemaInventarioV1NetCore.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventarioV1NetCore.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventarioV1NetCore.AccesoDatos.Repositorio
{
    internal class CategoriaRepositorio : Repositorio<Categoria>, ICategoriaRepositorio
    {
        private readonly ApplicationDbContext _db;

        public CategoriaRepositorio(ApplicationDbContext db):base(db) 
        {
            _db = db;
        }

        public void Actualizar(Categoria categoria)
        {
           var categoriaBD= _db.Categorias.FirstOrDefault(b=>b.Id==categoria.Id);

            if (categoriaBD != null)
            {
                categoriaBD.Nombre= categoria.Nombre;
                categoriaBD.Descripcion= categoria.Descripcion;
                categoriaBD.Estado= categoria.Estado;
                _db.SaveChanges();
            }
        }
    }
}
