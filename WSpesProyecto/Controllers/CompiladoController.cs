using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Microsoft.EntityFrameworkCore;
using WSpesProyecto.Models;

namespace WSpesProyecto.Controllers
{
    [EnableCors("Cors law")]
    [Route("api/[controller]")]
    [ApiController]
    public class CompiladoController : ControllerBase
    {
        public readonly PesproyectoDbContext context;

        public CompiladoController(PesproyectoDbContext contenido)
        {
            context = contenido;
        }

        [HttpGet]
        [Route("Compilado")]
        public IActionResult Compilado() 
        { 
            List<Compilado> compilados = new List<Compilado>();
            try
            {
                compilados=context.Compilados.Include(p=>p.oProductos).ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = compilados});
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = compilados });
            }
        }
        //metodo para filtrar por id compilado
        [HttpGet]
        [Route("Obtener/{IdCompilado:int}")]
        public IActionResult Obtener(int IdCompilado)
        {
            Compilado compilado = context.Compilados.Find(IdCompilado);
            if(compilado == null)
            {
                return BadRequest("Producto no encontrado");
            }
            try
            {
                //busque por el id y de vuelva el objeto dependiendo si  existe
                //se pone el include para incluir los datos de la tabla productos
                compilado = context.Compilados.Where(p => p.IdCompilado == IdCompilado).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "busqueda exitosa", compilado });
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, compilado });
            }
        }
        [HttpPost]//metodo para crear un nuevo Compilado
        [Route("Agregar")]
        public IActionResult Guardar([FromBody] Compilado obj)
        {
            try
            {
                context.Compilados.Add(obj);//adding Compilado
                context.SaveChanges();//guardar los Compilado
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }
        //metodo de editar
        [HttpPut]
        [Route("Editar")]
        public IActionResult Editar([FromBody] Compilado obj)
        {
            Compilado compilado = context.Compilados.Find(obj.IdCompilado);
            if (compilado == null)
            {
                return BadRequest("Articulo no editado");
            }
            try
            {
                compilado.Oficina= obj.Oficina is null ? compilado.Oficina: obj.Oficina;
                compilado.Area=obj.Area is null ? compilado.Area:obj.Area;
                compilado.CodigoPrograma= obj.CodigoPrograma is null ? compilado.CodigoPrograma: obj.CodigoPrograma;
                compilado.CodigoSubpartida= obj.CodigoSubpartida is null ? compilado.CodigoSubpartida: obj.CodigoSubpartida;
                compilado.IdProductos=obj.IdProductos is null ? compilado.IdProductos: obj.IdProductos;
                compilado.PeriodoEjecucion=obj.PeriodoEjecucion is null ? compilado.PeriodoEjecucion : obj.PeriodoEjecucion;
                compilado.Prioridad=obj.Prioridad is null ? compilado.Prioridad: obj.Prioridad;
                compilado.TipoTramite=obj.TipoTramite is null ? compilado.TipoTramite:obj.TipoTramite;
                compilado.NumeroContratoVigente= obj.NumeroContratoVigente is null ? compilado.NumeroContratoVigente: obj.NumeroContratoVigente;
                compilado.RequisionCertificacion=obj.RequisionCertificacion is null ? compilado.RequisionCertificacion : obj.RequisionCertificacion;
                compilado.NumeroScUcUa=obj.NumeroScUcUa is null ? compilado.NumeroScUcUa : obj.NumeroScUcUa;

                context.Compilados.Update(compilado);// se actualizan los cambios
                context.SaveChanges();// se guardan los cambios
                return StatusCode(StatusCodes.Status200OK, new { mensaje="ok" });
            } catch (Exception ex) {
                return StatusCode(StatusCodes.Status200OK, new { mensaje=ex.Message });
            }
        }
        //metodo de eliminar un compilado
        [HttpDelete]
        [Route("Eliminar/{IdCompilado:int}")]
        public IActionResult Eliminar(int IdCompilado)
        {
            Compilado nuevoProducto = context.Compilados.Find(IdCompilado); // para que busque el id del producto

            if (nuevoProducto == null)//para ver si existe
            {
                return BadRequest("Producto no encontrado");
            }
            try
            {

                context.Compilados.Remove(nuevoProducto);//actualiza el contenido del nuevo producto
                context.SaveChanges();//se guardan los cambios
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }
    }

}
