using FacturacionAPI_EF.Models;
using FacturacionAPI_EF.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FacturacionAPI_EF.Controllers
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

        private bool IsArticleValid(Article article)
        {
            return 
                !string.IsNullOrEmpty(article.Nombre) &&
                article.PreUnitario > 0;
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
                else
                {
                    _service.Create(value);
                    return Ok(new { mensaje = "Artículo creado con éxito!" });
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
            try
            {
                if (value == null || !IsArticleValid(value) || id != value.IdArticulo)
                {
                    return BadRequest(new { mensaje = "Artículo incorrecto" });
                }
                else
                {
                    _service.Update(value);
                    return Ok(new { mensaje = "Artículo actualizado con éxito!" });                  
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

        // DELETE api/<ArticlesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_service.GetById(id) != null)
            {
                try
                {
                    _service.Delete(id);
                    return Ok(new { mensaje = "Artículo eliminado con éxito!" });
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
