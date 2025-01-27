﻿using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;

namespace API.Repositories
{
    public interface IEmploymentTypeRepo
    {
        Task<IEnumerable<EmploymentType>> GetEmploymentTypeAsync();

        Task<EmploymentType> GetEmploymentTypeByIdAsync(int EmploymentTypeId);

        Task CreateEmploymentTypeAsync(EmploymentType employmentType);

        void UpdateEmploymentTypeAsync(int EmploymentTypeId, EmploymentType employmentType);

        void DeleteEmploymentType(int id);

        Task<bool> SaveAsync();
    }
}