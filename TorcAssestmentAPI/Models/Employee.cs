using System;
using System.Collections.Generic;

namespace TorcAssestmentAPI.Models;

public partial class Employee
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Position { get; set; } = null!;

    public decimal Salary { get; set; }

    public DateTime HireDate { get; set; }
}
