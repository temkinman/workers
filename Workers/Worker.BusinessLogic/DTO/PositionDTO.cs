using System.ComponentModel.DataAnnotations;

namespace Worker.BusinessLogic.DTO;

public class PositionDTO
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public int Grade { get; set; } = 1;
    public List<EmployeeDTO> Employees { get; set; } = new();
}
