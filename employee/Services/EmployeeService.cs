using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using employee.Models;
using employee.Repositories;

namespace employee.Services
{
    public class EmployeeService
    {
        private readonly EmployeeRepository _employeeRepository;

        public EmployeeService(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _employeeRepository.GetEmployees();
        }

        public async Task<Employee> GetEmployee(Guid id)
        {
            return await _employeeRepository.GetEmployee(id);

        }

        public async Task<int> AddEmployee(Employee employee)
        {
            return await _employeeRepository.AddEmployee(employee);
        }

        public async Task<int> UpdateEmployee(Employee employee)
        {

            return await _employeeRepository.UpdateEmployee(employee);
        }

        public async Task<int> DeleteEmployee(Employee employee)
        {

            return await _employeeRepository.DeleteEmployee(employee);
        }
    }
}
