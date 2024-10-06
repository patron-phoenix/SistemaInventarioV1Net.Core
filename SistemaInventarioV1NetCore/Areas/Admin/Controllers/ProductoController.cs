using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using SistemaInventarioV1NetCore.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventarioV1NetCore.Modelos;
using SistemaInventarioV1NetCore.Modelos.ViewModels;
using SistemaInventarioV1NetCore.Utilidades;

namespace SistemaInventarioV1NetCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = DS.Role_Admin  + "," + DS.Role_Inventario)]
    public class ProductoController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductoController(IUnidadTrabajo unidadTrabajo, IWebHostEnvironment webHostEnvironment)
        {
            _unidadTrabajo=unidadTrabajo;
            _webHostEnvironment=webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task< IActionResult> Upsert(int? id)
        {
            ProductoVM productoVM = new ProductoVM()
            {
                Producto = new Producto(),
                CategoriaLista = _unidadTrabajo.Producto.ObtenerTodosDropdownList("Categoria"),
                MarcaLista=_unidadTrabajo.Producto.ObtenerTodosDropdownList("Marca"),
                PadreLista = _unidadTrabajo.Producto.ObtenerTodosDropdownList("Producto")
            };

            if (id==null)
            {
                //crear nuevo producto
                productoVM.Producto.Estado = true;
                return View(productoVM);
            }
            else
            {
              productoVM.Producto = await _unidadTrabajo.Producto.Obtener(id.GetValueOrDefault());
                if (productoVM.Producto ==null)
                {
                    return NotFound();
                }
                return View(productoVM);
            }           
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Upsert(ProductoVM productoVM)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                if (productoVM.Producto.Id ==0)
                {
                    //crear
                    string upload = webRootPath + DS.ImagenRuta;
                    string fileName=Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(upload,fileName+extension),FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    productoVM.Producto.ImagenUrl = fileName + extension;
                    await _unidadTrabajo.Producto.Agregar(productoVM.Producto);
                }
                else
                {
                    //actualizar
                    var objProducto = await _unidadTrabajo.Producto.ObtenerPrimero(p=>p.Id == productoVM.Producto.Id, isTracking:false);
                    if (files.Count >0) //si se carga una nueva imagen para el producto existente
                    {
                        string upload=webRootPath+DS.ImagenRuta;
                        string fileName=Guid.NewGuid().ToString();
                        string extension=Path.GetExtension(files[0].FileName);

                        // borrar la imagen anterior
                        var anteriorFile=Path.Combine(upload,objProducto.ImagenUrl);
                        if (System.IO.File.Exists(anteriorFile))
                        {
                            System.IO.File.Delete(anteriorFile);
                        }

                        using (var fileStream = new FileStream(Path.Combine(upload,fileName+extension),FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }

                        productoVM.Producto.ImagenUrl=fileName+extension;
                    }//case contrario no se carga una nueva imagen
                    else
                    {
                        productoVM.Producto.ImagenUrl=objProducto.ImagenUrl;
                    }
                    _unidadTrabajo.Producto.Actualizar(productoVM.Producto);
                }

                TempData[DS.Exitosa] = "El Producto se guardo con exito...";
                await _unidadTrabajo.Guardar();
                return View(nameof(Index));
            }//if not valid
            productoVM.CategoriaLista = _unidadTrabajo.Producto.ObtenerTodosDropdownList("Categoria");
            productoVM.CategoriaLista = _unidadTrabajo.Producto.ObtenerTodosDropdownList("Marca");
            productoVM.PadreLista = _unidadTrabajo.Producto.ObtenerTodosDropdownList("Producto");
            return View(productoVM);
        }
        

        #region API

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.Producto.ObtenerTodos(incluirPropiedades:"Categoria,Marca");
            return Json(new {data=todos});
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var productoDb = await _unidadTrabajo.Producto.Obtener(id);
            if (productoDb == null)
            {
                return Json(new {success=false,message="Error al borrar Producto"});
            }
            //eliminar imagen
            string upload = _webHostEnvironment.WebRootPath + DS.ImagenRuta;
            var anteriorFile = Path.Combine(upload,productoDb.ImagenUrl);
            if (System.IO.File.Exists(anteriorFile))
            {
                System.IO.File.Delete(anteriorFile);
            }

            _unidadTrabajo.Producto.Remover(productoDb);
            await _unidadTrabajo.Guardar();
            return Json(new {success=true,message="Producto borrado exitosamente"});
        }


        [ActionName("ValidarSerie")]
        public async Task<IActionResult> ValidarSerie(string serie,int id=0)
        {
            bool valor = false;
            var lista = await _unidadTrabajo.Producto.ObtenerTodos();
            if (id==0)
            {
                valor = lista.Any(b=>b.NumeroSerie.ToLower().Trim()==serie.ToLower().Trim());
            }
            else
            {
                valor = lista.Any(b => b.NumeroSerie.ToLower().Trim() == serie.ToLower().Trim() && b.Id!=id);
            }

            if (valor)
            {
                return Json(new {data=true});
            }
            return Json(new {data=false});
        }

        #endregion


    }
}
