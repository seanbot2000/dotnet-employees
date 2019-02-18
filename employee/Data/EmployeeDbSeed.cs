using System;
using System.Threading.Tasks;
using employee.Models;
using employee.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace employee.Data
{

    public class EmployeeDbSeed
    { 
        private readonly EmployeeService _employeeService;
        private readonly SampleConfig _employeeConfiguration;
        private readonly ILogger _logger;

        public EmployeeDbSeed(EmployeeService employeeService, IOptionsSnapshot<SampleConfig> options, ILogger<EmployeeDbSeed> logger)
        {
            _employeeService = employeeService;
            _employeeConfiguration = options.Value;
            _logger = logger;
        }

        public async Task SeedEmployees()
        {
            _logger.LogError("Seeding Database");
            _logger.LogError("_employeeConfiguration = " + _employeeConfiguration.ToString());
            if (_employeeConfiguration.initialize)
            {
                Employee one = new Employee();
                one.firstName = _employeeConfiguration.firstNames[0];
                one.lastName = _employeeConfiguration.lastNames[0];
                one.email = _employeeConfiguration.emails[0];
                await _employeeService.AddEmployee(one);
                Employee two = new Employee();
                two.firstName = _employeeConfiguration.firstNames[1];
                two.lastName = _employeeConfiguration.lastNames[1];
                two.email = _employeeConfiguration.emails[1];
                await _employeeService.AddEmployee(two);
            }
        }
    }
}
