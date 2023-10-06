using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WSpesProyecto.Models;

namespace WSpesProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        public readonly PesproyectoDbContext contexto;

        public ProductosController(PesproyectoDbContext contenido)
        {
            contexto = contenido;
        }

        [HttpGet]//metodo get
        [Route("rutaApi")]//para llamar a la api
        public IActionResult rutaApi()
        {
            List<Productos> nuevoProducto = new List<Productos>();
            try
            {
                nuevoProducto = contexto.Productos.ToList();
                return StatusCode(StatusCodes.Status200OK, new {mensaje="ok", response=nuevoProducto});
            }catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = nuevoProducto });
            }
        }
        [HttpGet]// metodo para filtrar por id 
        [Route("Obtener/{IdProductos:int}")]
        public IActionResult Obtener(int IdProductos)
        {
            Productos nuevoProducto = contexto.Productos.Find(IdProductos); // para que busque el id del producto

            if (nuevoProducto==null)
            {
                return BadRequest("Producto no encontrado");
            }
            try
            {
                nuevoProducto = contexto.Productos.Where(p => p.IdProductos == IdProductos).FirstOrDefault();// me devolvera un objeto dependiendo si existe
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = nuevoProducto });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = nuevoProducto });
            }
        }

        [HttpPost]//metodo para crear un nuevo producto
        public IActionResult Guardar([FromBody] Productos obj)
        {
            try
            {
                contexto.Productos.Add(obj);//adding productos
                contexto.SaveChanges();//guardar los productos
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex) {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpPut]//metodo de editar
        [Route("Editar")]
        public IActionResult Editar([FromBody] Productos obj)
        {
            Productos nuevoProducto = contexto.Productos.Find(obj.IdProductos); // para que busque el id del producto

            if (nuevoProducto == null)//para ver si existe
            {
                return BadRequest("Producto no encontrado");
            }
            try
            {
                //este proceso verifica si todos los campos estan correctos
                nuevoProducto.Articulo = obj.Articulo is null ? nuevoProducto.Articulo : obj.Articulo;
                nuevoProducto.CodigoArticulo = obj.CodigoArticulo is null ? nuevoProducto.CodigoArticulo : obj.CodigoArticulo;
                nuevoProducto.Cantidad = obj.Cantidad is null ? nuevoProducto.Cantidad : obj.Cantidad;
                nuevoProducto.CostoUnitario = obj.CostoUnitario is null ? nuevoProducto.CostoUnitario : obj.CostoUnitario;
                nuevoProducto.MontoTotal = obj.MontoTotal is null ? nuevoProducto.MontoTotal : obj.MontoTotal;
                nuevoProducto.Descripcion = obj.Descripcion is null ? nuevoProducto.Descripcion : obj.Descripcion;
                contexto.Productos.Update(nuevoProducto);//actualiza el contenido del nuevo producto
                contexto.SaveChanges();//se guardan los cambios
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }
        [HttpDelete]//metodo de eliminar un producto
        [Route("Eliminar/{IdProductos:int}")]
        public IActionResult Eliminar(int IdProductos)
        {
            Productos nuevoProducto = contexto.Productos.Find(IdProductos); // para que busque el id del producto

            if (nuevoProducto == null)//para ver si existe
            {
                return BadRequest("Producto no encontrado");
            }
            try
            {
                
                contexto.Productos.Remove(nuevoProducto);//actualiza el contenido del nuevo producto
                contexto.SaveChanges();//se guardan los cambios
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }
    }
}
