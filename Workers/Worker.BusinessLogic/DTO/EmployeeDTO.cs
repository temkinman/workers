using System.ComponentModel.DataAnnotations;

namespace Worker.BusinessLogic.DTO;

public class EmployeeDTO
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [StringLength(50, ErrorMessage = "The FirstName value cannot be less 2 characters. ", MinimumLength = 2)]
    public required string FirstName { get; set; }

    [StringLength(50, ErrorMessage = "The LastName value cannot be less 2 characters. ", MinimumLength = 2)]
    public required string LastName { get; set; }
    public string? SurName { get; set; }
    public required DateTime BDay { get; set; }

    public List<PositionDTO> Positions { get; set; } = new();
}
