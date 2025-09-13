using Facturacion.Domain;
using Facturacion.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FacturacionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private IArticleService _service;

        public ArticlesController(IArticleService service)
        {
            _service = service;
        }

        private bool IsArticleValid(Article value)
        {
            return true;
        }

        // GET: api/<ArticlesController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_service.GetAll());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al acceder a los datos!!!" });
            }
        }

        // GET api/<ArticlesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_service.GetById(id));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al acceder al artículo!!!" });
            }
        }

        // POST api/<ArticlesController>
        [HttpPost]
        public IActionResult Post([FromBody] Article? value)
        {
            try
            {
                if (value == null || !IsArticleValid(value))
                {
                    return BadRequest(new { mensaje = "Articulo incorrecto" });
                }

                if (_service.Save(value))
                {
                    return Ok(new { mensaje = "Articulo registrado con exito" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                       new { mensaje = "No se pudo guardar el artículo, sin error específico" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    mensaje = ex.Message,
                    detalle = ex.InnerException?.Message
                });

            }
        }

        // PUT api/<ArticlesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Article? value)
        {

            if (_service.GetById(id) != null)
            {
                try
                {
                    if (value == null || !IsArticleValid(value))
                    {
                        return BadRequest(new { mensaje = "Articulo incorrecto" });
                    }

                    if (_service.Save(value))
                    {
                        return Ok(new { mensaje = "Articulo registrado con exito" });
                    }
                    else
                    {
                        throw new Exception("Error al grabar datos!");
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.ToString() });
                }
            } 
            else
            {
                throw new Exception("No existe un articulo con ese id! Debes hacer un POST");
            }
            
        }

        // DELETE api/<ArticlesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_service.GetById(id) != null)
            {
                try
                {
                    return Ok(_service.Delete(id));
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.ToString() });
                }
            }
            else
            {
                throw new Exception("No existe un producto con ese id");
            }
        }
    }
}
