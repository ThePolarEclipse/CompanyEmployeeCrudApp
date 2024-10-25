using Bogus;
using CompanyEmployeeCrudApp.Models;
using System.Linq;

namespace CompanyEmployeeCrudApp.Data
{
    public class DbSeeder
    {
        public static void Seed(AppDbContext context)
        {
            if (!context.Companies.Any())
            {
                // Create a Faker for Companies
                var companyFaker = new Faker<Company>()
                    .RuleFor(c => c.Name, f => f.Company.CompanyName())
                    .RuleFor(c => c.Email, f => f.Internet.Email())
                    .RuleFor(c => c.Logo, f => f.Image.PicsumUrl(100, 100))  // Generate a 100x100 logo image
                    .RuleFor(c => c.Website, f => f.Internet.Url());

                // Generate 10 companies
                var companies = companyFaker.Generate(10);

                // Add companies to the database and save changes
                context.Companies.AddRange(companies);
                context.SaveChanges();

                // Create a Faker for Employees
                var employeeFaker = new Faker<Employee>()
                    .RuleFor(e => e.FirstName, f => f.Name.FirstName())
                    .RuleFor(e => e.LastName, f => f.Name.LastName())
                    .RuleFor(e => e.Email, f => f.Internet.Email())
                    .RuleFor(e => e.PhoneNumber, f => f.Phone.PhoneNumber());

                // Generate 50 employees and assign them to random companies
                var employees = employeeFaker.Generate(50);
                foreach (var employee in employees)
                {
                    employee.CompanyId = context.Companies
                        .OrderBy(c => Guid.NewGuid()) 
                        .Select(c => c.Id)
                        .FirstOrDefault();
                }

                // Add employees to the database and save changes
                context.Employees.AddRange(employees);
                context.SaveChanges();
            }
        }
    }
}
