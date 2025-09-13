using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Facturacion.Data.Utils
{
    public class UnitOfWork : IDisposable
    {
        private readonly SqlConnection _connection;
        private SqlTransaction _transaction;
        private IBudgetRepository _budgetRepository;
        private bool _disposed = false; // bandera para asegurar de que Dispose no se ejecute 2 veces

        public UnitOfWork(string cnnString)
        {
            _connection = new SqlConnection(cnnString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public IBudgetRepository BudgetRepository
        {
            get
            {
                if(_budgetRepository == null)
                {
                    _budgetRepository = new BudgetRepository(_connection, _transaction);
                }
                return _budgetRepository;
            }
        }

        public void SaveChanges()
        {
            if (_transaction == null)
                throw new InvalidOperationException("No hay transacción activa.");
            try
            {
                _transaction.Commit();
            }
            catch(Exception ex) {
            
                _transaction.Rollback();
                throw new InvalidOperationException("Error al guardar cambios en la base de datos", ex);
            }
        }

        public void Dispose()
        {
            if (_disposed) return;

            _transaction?.Dispose();
            _connection?.Close();
            _connection?.Dispose();

            _disposed = true;

        }
    }
}
