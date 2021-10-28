using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace _03.EntityFrameworkCore_Introduction
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await using (var dbContext = new SoftUniiContext())
            {
                //first demo
                Employees employee1 = await dbContext.Employees.FindAsync(4);
                Employees employee = await dbContext.Employees
                                               .FirstOrDefaultAsync(e => e.EmployeeId == 1);
                Console.WriteLine($"{employee.FirstName} {employee.LastName}");
                Console.WriteLine("................................................");

                //Second demo, annonimous object (can't be edited, it's read only)
                var employees = dbContext.Employees
                                                     .Where(e => e.Department.Name == "Marketing")
                                                     .Select(e => new
                                                     {
                                                         Name = $"{e.FirstName} {e.LastName}",
                                                         Department = e.Department.Name
                                                     })
                                                     .ToQueryString(); //generates sql query (the one from the profiler)

                Console.WriteLine(employees);
                 
                Console.WriteLine("................................................");
            }
        }
    }
}
