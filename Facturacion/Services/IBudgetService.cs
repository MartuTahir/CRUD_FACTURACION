﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facturacion.Domain;

namespace Facturacion.Services
{
    public interface IBudgetService
    {
        bool Insert(Budget budget);
        bool Update(Budget budget);
        bool Delete(int id);
        List<Budget> GetAll();
        Budget GetById(int id);
    }
}
