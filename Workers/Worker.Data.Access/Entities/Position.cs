using System.ComponentModel.DataAnnotations;

namespace Worker.Data.Access.Entities;

public class Position
{
    public Position()
    {
        Employees = new HashSet<Employee>();
    }

    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }

    [Range(1, 15, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
    public required int Grade { get; set; } = 1;
    public virtual ICollection<Employee> Employees { get; set; }
    public virtual List<EmployeePosition> EmployeePositions { get; set; }
}
