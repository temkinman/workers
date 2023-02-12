using System.ComponentModel.DataAnnotations;

namespace Worker.BusinessLogic.DTO;

public class EmployeeDTO
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? SurName { get; set; }
    public required DateTime BDay { get; set; }

    public List<PositionDTO> Positions { get; set; } = new();
}
