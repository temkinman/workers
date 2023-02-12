using System.ComponentModel.DataAnnotations;

namespace Worker.Data.Access.Entities;

public class Employee
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [StringLength(50, ErrorMessage = "The FirstName value cannot be less 2 characters. ", MinimumLength = 2)]
    public required string FirstName { get; set; }

    [StringLength(50, ErrorMessage = "The FirstName value cannot be less 2 characters. ", MinimumLength = 2)]
    public required string LastName { get; set; }
    public string? SurName { get; set; }
    public required DateTime BDay { get; set; }

    List<Position> Positions { get; set; } = new();
}
