using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using employee.Models;
using System.Linq;

namespace employee.Data
{
    public static class EmployeeDbInitialize
    {
        public static void init(IServiceProvider serviceProvider)
        {
            using (var context = new EmployeeDbContext(serviceProvider.GetRequiredService<DbContextOptions<EmployeeDbContext>>()))
            {
                context.Database.EnsureCreated();

                if (context.Employees.Any()) return;

                //context.Employees.Add(new Employee
                //{
                //    firstName = "DaShaun",
                //    lastName = "Carter",
                //    email = "dcarter@pivotal.io"
                //});
                //context.Employees.Add(new Employee
                //{
                //    firstName = "Sean",
                //    lastName = "Noyes",
                //    email = "snoyes@pivotal.io"
                //});
                //context.SaveChanges();
            }
        }
    }
}