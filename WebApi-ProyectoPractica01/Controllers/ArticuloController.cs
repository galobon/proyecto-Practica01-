using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using proyectoPratica01.Dominio;
using proyectoPratica01.Servicios;

namespace WebApi_ProyectoPractica01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticuloController : ControllerBase
    {
        private ArticuloServicio _service;

        public ArticuloController()
        {
            _service = new ArticuloServicio();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var articulos = _service.GetArticulos();
                return Ok(articulos);
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
                var articulo = _service.GetArticulo(id);
                if (articulo == null)
                    return BadRequest($"No existe un articulo con la ID: {id}");
                return Ok(articulo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"No se pudo procesar la peticion: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult Post(Articulo articulo)
        {
            try
            {
                bool result = _service.SaveArticulo(articulo);
                if (!result)
                    return BadRequest("Articulo con datos invalidos");
                return Ok("Se cargo el articulo con exito!!!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"No se pudo procesar la peticion: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Articulo articulo)
        {
            try
            {
                articulo.IdArticulo = id;
                bool result = _service.UpdateArticulo(articulo);
                if (!result)
                    return BadRequest("Articulo con datos invalidos");
                return Ok("Se actualizo el artículo con exito!!!");
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
                    return BadRequest("Articulo no encontrado");
                return Ok("Se borro el articulo con exito!!!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"No se pudo procesar la peticion: {ex.Message}");
            }
        }
    }
}
