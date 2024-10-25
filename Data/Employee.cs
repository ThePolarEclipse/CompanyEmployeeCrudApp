namespace CompanyEmployeeCrudApp.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public int CompanyId { get; set; }
        public required Company Company { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
    }
}
