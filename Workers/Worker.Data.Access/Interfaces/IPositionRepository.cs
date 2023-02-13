using Worker.Data.Access.Entities;

namespace Worker.Data.Access.Interfaces;
public interface IPositionRepository
{
    Task<Position?> GetPositionAsync(Guid id);
    Task<Position?> CreatePositionAsync(Position position);
    Task<Position?> UpdatePositionAsync(Position position);
    Task DeletePositionAsync(Guid id);
    Task<bool> IsPositionExistAsync(string name, int grade);
    Task<List<Position>> GetAllPositionsAsync();
}
