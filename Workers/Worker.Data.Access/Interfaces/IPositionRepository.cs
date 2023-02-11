using Worker.Data.Access.Entities;

namespace Worker.Data.Access.Interfaces;
public interface IPositionRepository
{
    Task<Employee> GetEmployeesByPositionAsync(Guid id);
    Task<Position> CreatePositionAsync(Position position);
    Task<Position> UpdatePositionAsync(Position position);
    Task DeletePositionAsync(Guid id);
}
