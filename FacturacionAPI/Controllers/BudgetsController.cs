using Facturacion.Data.Utils;
using Facturacion.Domain;
using Facturacion.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FacturacionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetsController : ControllerBase
    {
        private readonly IBudgetService _service;

        public BudgetsController(IBudgetService service)
        {
            _service = service;
        }


        // GET: api/<BudgetsController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var budgets = Ok(_service.GetAll());
                return Ok(budgets);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al acceder a los datos!!!" });
            }
        }

        // GET api/<BudgetsController>/5
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

        // POST api/<BudgetsController>
        [HttpPost]
        public IActionResult Post([FromBody] Budget value)
        {
            try
            {
                if (value == null || value.Details == null || !value.Details.Any())
                    return BadRequest("Se espera una factura con al menos un detalle.");

                bool result = _service.Insert(value);
                return result ? Ok("Factura registrada con éxito") : StatusCode(500, "No se pudo registrar la factura");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = ex.Message });
            }

        }

        // DELETE api/<BudgetsController>/5
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
