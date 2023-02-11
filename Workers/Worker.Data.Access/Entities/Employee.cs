using System.ComponentModel.DataAnnotations;

namespace Worker.Data.Access.Entities;

public class Employee
{
    public Employee(string firstName, string lastName, string surName, DateTime bDay)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        SurName = surName;
        BDay = bDay;
    }

    public Guid Id { get; init; }

    [StringLength(50, ErrorMessage = "The FirstName value cannot be less 2 characters. ", MinimumLength = 2)]
    public required string FirstName { get; init; }

    [StringLength(50, ErrorMessage = "The FirstName value cannot be less 2 characters. ", MinimumLength = 2)]
    public required string LastName { get; init; }
    public string? SurName { get; init; }
    public required DateTime BDay { get; init; }

    List<Position> Positions { get; set; } = new List<Position>();
}
