using System.ComponentModel.DataAnnotations;

namespace Worker.BusinessLogic.DTO;

public class PositionDTO
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }

    [Range(1, 15, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
    public int Grade { get; set; } = 1;
    public List<EmployeeDTO> Employees { get; set; } = new();
}
