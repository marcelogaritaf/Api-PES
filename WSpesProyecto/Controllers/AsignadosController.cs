using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using WSpesProyecto.Models;

namespace WSpesProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsignadosController : ControllerBase
    {
        public readonly PesproyectoDbContext context;
        
        public AsignadosController(PesproyectoDbContext contenido)
        {
            context = contenido;
        }
        //metodo para obtener los datos generales de asignados
        [HttpGet]
        [Route("Obtener")]
        public IActionResult asignados()
        {
            List<Asignados> nuevaAsignacion = new List<Asignados>();
            try
            {
                nuevaAsignacion=context.Asignados.Include(c=> c.oCompilado).ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje= "ok", response= nuevaAsignacion}) ;
            }catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = nuevaAsignacion });
            }
        }
        //metodo get por IdAsignados
        [HttpGet]
        [Route("Obtener/{IdAsignados:int}")]
        public IActionResult get(int IdAsignados)
        {
            Asignados asignados = context.Asignados.Find(IdAsignados);
            if(asignados == null)
            {
                return BadRequest("Asignacion no encontrada");
            }
            try
            {
                asignados = context.Asignados.Where(a => a.IdAsignados == IdAsignados).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "busqueda realizada correctamente", asignados });
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, asignados });

            }
        }
        //metodo post para crear asignaciones
        [HttpPost]
        [Route("Agregar")]
        public IActionResult add([FromBody] Asignados asignados)
        {
            try
            {
                context.Asignados.Add(asignados);
                context.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "asignacion agregada" });
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }
        //metodo put para editar las asignaciones
        [HttpPut]
        [Route("Editar")]
        public IActionResult editar([FromBody] Asignados obj)
        {
            Asignados asignados = context.Asignados.Find(obj.IdAsignados);
            if(asignados == null)
            {
                return BadRequest("La asignacion fue incorrecta");
            }
            try
            {
                asignados.IdCompilado = obj.IdCompilado is null ? asignados.IdCompilado:obj.IdCompilado;
                asignados.IdProductos = obj.IdProductos is null ? asignados.IdProductos:obj.IdProductos;
                asignados.Estado=obj.Estado is null ? asignados.Estado:obj.Estado;
                asignados.FechaInicio=obj.FechaInicio is null ? asignados.FechaInicio:obj.FechaInicio;
                asignados.FechaFin=obj.FechaFin is null ? asignados.FechaFin:obj.FechaFin;
                asignados.NombrePersona =obj.NombrePersona is null ? asignados.NombrePersona :obj.NombrePersona;
                asignados.Correo= obj.Correo is null ? asignados.Correo :obj.Correo;
                context.Asignados.Update(asignados);
                context.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "asignacion editada" });
            }catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message});

            }
        }
        //metodo delete para eliminar las asignaciones
        [HttpDelete]
        [Route("Eliminar/{IdAsignados:int}")]
        public IActionResult delete(int IdAsignados)
        {
            Asignados asignados = context.Asignados.Find(IdAsignados);
            if (IdAsignados == null)
            {
                return BadRequest("No se realizo la peticion");
            }
            try
            {
                context.Asignados.Remove(asignados);
                context.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "asignacion eliminada" });
            }catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message});
            }
        }
    }
}
