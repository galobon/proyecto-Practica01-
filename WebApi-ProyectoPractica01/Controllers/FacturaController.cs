using proyectoPratica01.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using proyectoPratica01.Dominio;

namespace WebApi_ProyectoPractica01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private FacturaServicio _service;

        public FacturaController()
        {
            _service = new FacturaServicio();
        }


        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var facturas = _service.GetFacturas();
                return Ok(facturas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"No se pudo procesar la petición: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var factura = _service.GetFactura(id);
                if (factura == null)
                    return BadRequest($"No existe una factura con la ID: {id}");
                return Ok(factura);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"No se pudo procesar la petición: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult Post(Factura factura)
        {
            try
            {
                bool result = _service.SaveFactura(factura);
                if (!result)
                    return BadRequest("Factura con datos invalidos");
                return Ok("Se cargo la factura con exito!!!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"No se pudo procesar la petición: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Factura factura)
        {
            try
            {
                bool result = _service.UpdateFactura(id, factura);
                if (!result)
                    return BadRequest("Factura con datos invalidos");
                return Ok("Se cargo la factura con exito!!!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"No se pudo procesar la petición: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                bool result = _service.DeleteFactura(id);
                if (!result)
                    return BadRequest("Factura no encontrada");
                return Ok("Se borro la factura con exito!!!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"No se pudo procesar la petición: {ex.Message}");
            }
        }
    }
}
