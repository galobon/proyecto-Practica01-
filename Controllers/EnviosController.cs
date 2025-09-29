using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModeloParcial1PII.DTOs;
using ModeloParcial1PII.Servicios;

namespace ModeloWEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnviosController : ControllerBase
    {
        private readonly IEnvioServicio _envioServicios;

        public EnviosController(IEnvioServicio envioServicios)
        {
            _envioServicios = envioServicios;
        }

        [HttpPost("filtros")]
        public IActionResult GetEnviosByFilters(EnvioFiltrosDTO filtros)
        {
            try
            {
                var Envios = _envioServicios.GetEnviosByFilters(filtros);

                if (Envios.Count == 0)
                {
                    return NotFound("No se pudo encontrar ningun producto que corresponda al criterio de busqueda");
                }
                return Ok(Envios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del serviodor: {ex}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult CancelEnvio(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Debe ingresar una id mayor a 0");
                }
                if (_envioServicios.CancelEnvio(id))
                {
                    return Ok("Se cancelo el pedido con exito!!");
                }
                return NotFound($"No se encontro un envio con la id: {id}");

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del serviodor: {ex}");
            }
        }
    }
}
