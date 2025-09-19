using FacturacionAPI_EF.Models;
using FacturacionAPI_EF.Models.DTOs;
using FacturacionAPI_EF.Services;
using Microsoft.AspNetCore.Mvc;

namespace FacturacionAPI_EF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetsController : ControllerBase
    {
        private readonly IBudgetService _budgetService;

        public BudgetsController(IBudgetService budgetService)
        {
            _budgetService = budgetService;
        }

        // GET: api/<BudgetsController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var budgets = await _budgetService.GetAllAsync();
                // Mapea las facturas a DTOs para enviar al json
                var budgetDtos = budgets.Select(b => new BudgetDto
                {
                    Id = b.Id,
                    Cliente = b.Cliente,
                    FormaPago = b.FormaPago,
                    Fecha = b.Fecha,
                    EstaActiva = b.EstaActiva,
                    DetallesFacturas = b.DetallesFacturas?.Select(d => new BudgetDetailDto
                    {
                        IdDetalle = d.IdDetalle,
                        IdFactura = d.IdFactura,
                        IdArticulo = d.IdArticulo,
                        Cantidad = d.Cantidad
                    }).ToList() ?? new List<BudgetDetailDto>()
                }).ToList();
                // Devuelve la lista de facturas en formato JSON
                return Ok(budgetDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // GET api/<BudgetsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var budget = await _budgetService.GetByIdAsync(id);
                if (budget == null)
                    return NotFound();
                // Mapea la factura a DTO
                var budgetDto = new BudgetDto
                {
                    Id = budget.Id,
                    Cliente = budget.Cliente,
                    FormaPago = budget.FormaPago,
                    Fecha = budget.Fecha,
                    EstaActiva = budget.EstaActiva,
                    DetallesFacturas = budget.DetallesFacturas?.Select(d => new BudgetDetailDto
                    {
                        IdDetalle = d.IdDetalle,
                        IdFactura = d.IdFactura,
                        IdArticulo = d.IdArticulo,
                        Cantidad = d.Cantidad
                    }).ToList() ?? new List<BudgetDetailDto>()
                };
                return Ok(budgetDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // POST api/<BudgetsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BudgetDto budgetDto)
        {
            try
            {
                if (budgetDto == null)
                    return BadRequest();
                // Mapea el DTO a entidad Budget
                var budget = new Budget
                {
                    Cliente = budgetDto.Cliente,
                    FormaPago = budgetDto.FormaPago,
                    Fecha = budgetDto.Fecha,
                    EstaActiva = budgetDto.EstaActiva,
                    DetallesFacturas = budgetDto.DetallesFacturas?.Select(d => new BudgetDetail
                    {
                        IdDetalle = d.IdDetalle,
                        IdFactura = d.IdFactura,
                        IdArticulo = d.IdArticulo,
                        Cantidad = d.Cantidad
                    }).ToList()
                };

                await _budgetService.AddAsync(budget);
                // Asigna el id generado al DTO
                budgetDto.Id = budget.Id;
                // Devuelve la factura creado con el código 201
                return CreatedAtAction(nameof(Get), new { id = budget.Id }, budgetDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // PUT api/<BudgetsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] BudgetDto budgetDto)
        {
            try
            {
                if (budgetDto == null || budgetDto.Id != id)
                    return BadRequest();

                var existingBudget = await _budgetService.GetByIdAsync(id);
                if (existingBudget == null)
                    return NotFound();
                // Actualiza los campos de la factura
                existingBudget.Cliente = budgetDto.Cliente;
                existingBudget.FormaPago = budgetDto.FormaPago;
                existingBudget.Fecha = budgetDto.Fecha;
                existingBudget.EstaActiva = budgetDto.EstaActiva;
                existingBudget.DetallesFacturas = budgetDto.DetallesFacturas?.Select(d => new BudgetDetail
                {
                    IdDetalle = d.IdDetalle,
                    IdFactura = d.IdFactura,
                    IdArticulo = d.IdArticulo,
                    Cantidad = d.Cantidad
                }).ToList();

                await _budgetService.UpdateAsync(existingBudget);

                return NoContent();
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // DELETE api/<BudgetsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var existingBudget = await _budgetService.GetByIdAsync(id);
                if (existingBudget == null)
                    return NotFound();

                await _budgetService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
