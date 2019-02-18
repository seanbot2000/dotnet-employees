using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using employee.Data;
using employee.Models;
using Microsoft.EntityFrameworkCore;

namespace employee.Repositories
{
    public class EmployeeRepository
    {
        private readonly EmployeeDbContext _employeeDbContext;

        public EmployeeRepository(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _employeeDbContext.Employees.ToListAsync();
        }

        public async Task<Employee> GetEmployee(Guid id)
        {
            return await _employeeDbContext.FindAsync<Employee>(id);

        }

        public async Task<int> AddEmployee(Employee employee)
        {
            _employeeDbContext.Add(employee);
            return await _employeeDbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateEmployee(Employee employee)
        {
            _employeeDbContext.Employees.Update(employee);

            return await _employeeDbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteEmployee(Employee employee)
        {
            _employeeDbContext.Employees.Remove(employee);

            return await _employeeDbContext.SaveChangesAsync();
        }
    }
}
