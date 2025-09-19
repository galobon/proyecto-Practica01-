using LOCURA.Dominio;
using LOCURA.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI_Practica01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagoController : ControllerBase
    {
        private PagoServicio _service;

        public PagoController()
        {
            _service = new PagoServicio();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var formasPago = _service.GetArticulos();
                return Ok(formasPago);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"No se pudo procesar la peticion: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var formaPago = _service.GetArticulo(id);
                if (formaPago == null)
                    return BadRequest($"No existe la forma de pago con ID: {id}");
                return Ok(formaPago);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"No se pudo procesar la peticion: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult Post(FormaPago fp)
        {
            try
            {
                bool result = _service.SaveArticulo(fp);
                if (!result)
                    return BadRequest("Forma de pago con datos invalidos");
                return Ok("Se cargo la forma de pago con exito!!!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"No se pudo procesar la peticion: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] FormaPago fp)
        {
            try
            {
                fp.Id = id;
                bool result = _service.UpdateArticulo(fp);
                if (!result)
                    return BadRequest("Forma de pago con datos invalidos");
                return Ok("Se actualizo la forma de pago con exito!!!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"No se pudo procesar la peticion: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                bool result = _service.DeleteArticulo(id);
                if (!result)
                    return BadRequest("Forma de pago no encontrada");
                return Ok("Se borro la forma de pago con exito!!!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"No se pudo procesar la peticion: {ex.Message}");
            }
        }
    }
}
