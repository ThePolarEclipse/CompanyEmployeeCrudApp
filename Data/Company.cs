namespace CompanyEmployeeCrudApp.Models
{
    public class Company
    {
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Logo { get; set; }
    public required string Website { get; set; }
    public List<Employee> Employees { get; set; } = new();
    }
}
