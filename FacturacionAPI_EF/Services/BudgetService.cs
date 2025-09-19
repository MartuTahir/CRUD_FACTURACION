using FacturacionAPI_EF.Data.Repositories;
using FacturacionAPI_EF.Models;

namespace FacturacionAPI_EF.Services
{
    public class BudgetService : IBudgetService
    {
        private readonly IBudgetRepository _budgetRepository;

        public BudgetService(IBudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }

        public async Task<IEnumerable<Budget>> GetAllAsync()
        {
            try
            {
                return await _budgetRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las facturas", ex);
            }
        }

        public async Task<Budget?> GetByIdAsync(int id)
        {
            try
            {
                return await _budgetRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener la factura con ID {id}", ex);
            }
        }

        public async Task AddAsync(Budget budget)
        {
            try
            {
                await _budgetRepository.AddAsync(budget);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar la factura", ex);
            }
        }

        public async Task UpdateAsync(Budget budget)
        {
            try
            {
                await _budgetRepository.UpdateAsync(budget);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la factura", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await _budgetRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar la factura con ID {id}", ex);
            } 
        }
    }
}
