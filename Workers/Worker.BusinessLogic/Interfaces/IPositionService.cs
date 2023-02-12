using Worker.BusinessLogic.DTO;

namespace Worker.BusinessLogic.Interfaces;

public interface IPositionService
{
    Task<PositionDTO?> GetPositionAsync(Guid id);
    Task<PositionDTO?> CreatePositionAsync(PositionDTO position);
    Task<PositionDTO?> UpdatePositionAsync(PositionDTO position);
    Task DeletePositionAsync(Guid id);
}
