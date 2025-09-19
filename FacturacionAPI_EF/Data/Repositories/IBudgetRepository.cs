using FacturacionAPI_EF.Models;

namespace FacturacionAPI_EF.Data.Repositories
{
    public interface IBudgetRepository
    {
        Task<IEnumerable<Budget>> GetAllAsync();
        Task<Budget?> GetByIdAsync(int id);
        Task AddAsync(Budget budget);
        Task UpdateAsync(Budget budget);
        Task DeleteAsync(int id);
    }
}
