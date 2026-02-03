namespace TorcAssestmentAPI.Models
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Position { get; set; } = null!;
        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; }
    }
}
