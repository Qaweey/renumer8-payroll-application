﻿using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly Remuner8Context context;

        public EmployeeRepository(Remuner8Context context)
        {
            this.context = context;
        }

        public async Task<int> EmployeeCountAsync()
        {
            var employeecount = await context.EmployeeBiodatas.CountAsync();
            return employeecount;
        }

        public async Task<IEnumerable<EmployeeBiodata>> GetAllEmployeesAsync()
        {
            return await context.EmployeeBiodatas.ToListAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return await context.SaveChangesAsync() >= 0;
        }
    }
}