using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using employee.Data;
using employee.Models;
using employee.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace employee.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class employeesController : ControllerBase
    {
        private readonly EmployeeService _employeeService;
        private readonly SampleConfig _employeeConfiguration;

        public employeesController(EmployeeService employeeService, IOptionsSnapshot<SampleConfig> options)
        {
            _employeeService = employeeService;
            _employeeConfiguration = options.Value;
        }

        [HttpGet]
        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            return await _employeeService.GetEmployees();
        }

        [HttpGet]
        [Route("{guid}")]
        public async Task<Employee> GetEmployee(Guid guid)
        {
            return await _employeeService.GetEmployee(guid);
        }

        [HttpPost]
        public async Task<int> Post(Employee employee)
        {
            return await _employeeService.AddEmployee(employee);
        }

        [HttpPut]
        public async Task<int> Put(Employee employee)
        {
            return await _employeeService.UpdateEmployee(employee);
        }

        [HttpDelete] async Task<int> Delete(Employee employee)
        {
            return await _employeeService.DeleteEmployee(employee);
        }
    }
}
