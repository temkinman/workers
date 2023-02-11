using System.ComponentModel.DataAnnotations;

namespace Worker.Data.Access.Entities;

public class Position
{
    public Position(string name, int grade = 1)
    {
        Id = Guid.NewGuid();
        Name = name;
        Grade = grade;
    }

    public Guid Id { get; init; }
    public required string Name { get; init; }

    [Range(1, 15, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
    public int Grade { get; init; }
    List<Employee> Employees { get; set; } = new List<Employee>();
}
