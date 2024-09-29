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
    internal class UsuarioAplicacionRepositorio : Repositorio<UsuarioAplicacion>, IUsuarioAplicacionRepositorio
    {
        private readonly ApplicationDbContext _db;

        public UsuarioAplicacionRepositorio(ApplicationDbContext db):base(db) 
        {
            _db = db;
        }

    }
}
