using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Facturacion.Data;
using Facturacion.Data.Utils;
using Facturacion.Domain;

namespace Facturacion.Services
{
    public class BudgetService : IBudgetService
    {
        private readonly string _connectionString;
        public BudgetService(IOptions<DbSettings> options)
        {
            _connectionString = options.Value.ConnectionString;
        }


        // obtener todas las facturas
        public List<Budget> GetAll()
        {
            using (var uow = new UnitOfWork(_connectionString))
            {
                return uow.BudgetRepository.GetAll();
            }
        }

        // obtener factura por id
        public Budget GetById(int id)
        {
            using (var uow = new UnitOfWork(_connectionString))
            {
                return uow.BudgetRepository.GetById(id);
            }
        }

        // insertar nueva factura
        public bool Insert(Budget budget)
        {
            using (var uow = new UnitOfWork(_connectionString))
            {
                try
                {
                    var repository = uow.BudgetRepository; // llamar al respositorio del unitOfWork para hacer el insert
                    
                    bool result = repository.Insert(budget);
                    uow.SaveChanges();

                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en Insert de servicio: {ex.Message}");
                    throw; // relanza para ver el stacktrace en la respuesta // rollback lo maneja UnitOfWork
                }
            }
        }

        // actualizar factura completa y sus detalles
        public bool Update(Budget budget)
        {
            using (var uow = new UnitOfWork(_connectionString))
            {
                try
                {
                    var repo = uow.BudgetRepository; // llamar al respositorio del unitOfWork para hacer el update
                    bool result = repo.Update(budget);

                    if (!result) return false;

                    uow.SaveChanges(); // commit
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        //eliminar factura completa y sus detalles en cascada (eso ya esta definido en sql)
        public bool Delete(int id)
        {
            using (var uow = new UnitOfWork(_connectionString))
            {
                try
                {
                    var repo = uow.BudgetRepository;// llamar al respositorio del unitOfWork para hacer el delete
                    bool result = repo.Delete(id);

                    if (!result) return false;

                    uow.SaveChanges(); // commit
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
