using Worker.BusinessLogic.DTO;
using Worker.BusinessLogic.Interfaces;

namespace Worker.BusinessLogic.Services;

public class PositionService : IPositionService
{
    public Task<PositionDTO?> CreatePositionAsync(PositionDTO position)
    {
        throw new NotImplementedException();
    }

    public Task DeletePositionAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<PositionDTO?> GetPositionAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<PositionDTO?> UpdatePositionAsync(PositionDTO position)
    {
        throw new NotImplementedException();
    }
}
