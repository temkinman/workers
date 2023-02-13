using Worker.BusinessLogic.DTO;
using Worker.Data.Access.Entities;

namespace Worker.BusinessLogic.Interfaces;

public interface IPositionService
{
    Task<PositionDTO?> GetPositionAsync(Guid id);
    Task<PositionDTO?> GetPositionByNameAsync(string name);
    Task<PositionDTO?> CreatePositionAsync(PositionDTO position);
    Task<PositionDTO?> UpdatePositionAsync(PositionDTO position);
    Task<bool> DeletePositionAsync(Guid id);
    Task<List<PositionDTO>> GetAllPositionsAsync();
}
