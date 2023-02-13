using AutoMapper;
using Worker.BusinessLogic.DTO;
using Worker.BusinessLogic.Interfaces;
using Worker.Data.Access.Entities;
using Worker.Data.Access.Interfaces;
using Worker.Data.Access.Repositories;

namespace Worker.BusinessLogic.Services;

public class PositionService : IPositionService
{
    private readonly IPositionRepository _positionRepository;
    private readonly IMapper _mapper;

    public PositionService(IPositionRepository positionRepository, IMapper mapper)
    {
        _positionRepository = positionRepository;
        _mapper = mapper;
    }

    public async Task<PositionDTO?> CreatePositionAsync(PositionDTO positionDto)
    {
        if (positionDto != null)
        {
            Position position = _mapper.Map<Position>(positionDto);

            var result = await _positionRepository.CreatePositionAsync(position);

            if (result == null)
            {
                return null;
            }

            return positionDto;
        }

        return null;
    }

    public async Task<bool> DeletePositionAsync(Guid id)
    {
        return await _positionRepository.DeletePositionAsync(id);
    }

    public async Task<List<PositionDTO>> GetAllPositionsAsync()
    {
        List<Position> positions = await _positionRepository.GetAllPositionsAsync();

        return _mapper.Map<List<Position>, List<PositionDTO>>(positions);
    }

    public async Task<PositionDTO?> GetPositionAsync(Guid id)
    {
        Position? position = await _positionRepository.GetPositionAsync(id);

        if (position == null)
        {
            return null;
        }

        return _mapper.Map<Position, PositionDTO>(position);
    }

    public async Task<PositionDTO?> GetPositionByNameAsync(string name)
    {
        Position? position = await _positionRepository.GetPositionByNameAsync(name);

        if (position == null)
        {
            return null;
        }

        return _mapper.Map<Position, PositionDTO>(position);
    }

    public async Task<PositionDTO?> UpdatePositionAsync(PositionDTO positionDto)
    {
        if (positionDto != null)
        {
            Position position = _mapper.Map<PositionDTO, Position>(positionDto);

            var result = await _positionRepository.UpdatePositionAsync(position);

            if (result == null)
            {
                return null;
            }

            return positionDto;
        }

        return null;
    }
}
