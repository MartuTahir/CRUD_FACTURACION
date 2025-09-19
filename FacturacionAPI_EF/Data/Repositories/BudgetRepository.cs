using FacturacionAPI_EF.Models;
using Microsoft.EntityFrameworkCore;

namespace FacturacionAPI_EF.Data.Repositories
{
    public class BudgetRepository : IBudgetRepository
    {
        private readonly LibraryContext _context;

        public BudgetRepository(LibraryContext context)
        {
            _context = context;
        }

        // Obtiene todos los registros de Budget, incluyendo sus detalles
        public async Task<IEnumerable<Budget>> GetAllAsync()
        {
            return await _context.Facturas
                .Include(b => b.DetallesFacturas) // Carga los detalles relacionados de forma anticipada
                .ToListAsync();
        }

        // Obtiene un Budget por su ID, incluyendo sus detalles
        public async Task<Budget?> GetByIdAsync(int id)
        {
            return await _context.Facturas
                .Include(b => b.DetallesFacturas) // Carga los detalles relacionados de forma anticipada
                .FirstOrDefaultAsync(b => b.Id == id); // Busca el Budget con el ID especificado
        }

        // Agrega un nuevo Budget a la base de datos
        public async Task AddAsync(Budget budget)
        {
            _context.Facturas.Add(budget); // Agrega la entidad Budget al contexto
            await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos
        }

        // Actualiza un Budget existente en la base de datos
        public async Task UpdateAsync(Budget budget)
        {
            _context.Facturas.Update(budget); // Marca la entidad Budget como modificada
            await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos
        }

        // Elimina un Budget de la base de datos por su ID
        public async Task DeleteAsync(int id)
        {
            var budget = await _context.Facturas.FindAsync(id); // Busca el Budget por ID
            if (budget != null)
            {
                _context.Facturas.Remove(budget); // Elimina la entidad Budget del contexto
                await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos
            }
        }
    }
}
