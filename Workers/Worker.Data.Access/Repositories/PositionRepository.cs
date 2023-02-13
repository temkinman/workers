using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Globalization;
using Worker.Data.Access.EF;
using Worker.Data.Access.Entities;
using Worker.Data.Access.Interfaces;

namespace Worker.Data.Access.Repositories;

public class PositionRepository : IPositionRepository
{
    private readonly WorkerAppDbContext _dbContext;
    private readonly ILogger<PositionRepository> _logger;

    public PositionRepository(WorkerAppDbContext dbContext, ILogger<PositionRepository> logger)
    {
        _dbContext= dbContext;
        _logger = logger;
    }

    public async Task<Position?> CreatePositionAsync(Position position)
    {
        _logger.LogInformation($"Trying create position with id {position.Id}");

        bool isExistPosition = await IsPositionExistAsync(position.Name, position.Grade);

        if(isExistPosition) 
        {
            _logger.LogWarning($"Position with name '{position.Name}' is already exist");
            return null;
        }

        try
        {
            _dbContext.Add(position);
            await _dbContext.SaveChangesAsync();
            return position;
        }
        catch (Exception ex)
        {
            _logger.LogWarning($"Position wasn't created: {ex.Message}");
            return null;
        }
    }

    public async Task DeletePositionAsync(Guid id)
    {
        _logger.LogInformation($"Trying to delete position with id {id}");

        try
        {
            Position? position = await _dbContext.Positions
                .Include(p => p.Employees)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (position != null && !position.Employees.Any())
            {
                _dbContext.Remove(position);
                await _dbContext.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning($"Position wasn't deleted: {ex.Message}");
        }
    }

    public async Task<List<Position>> GetAllPositionsAsync()
    {
        return await _dbContext.Positions
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Position?> GetPositionAsync(Guid id)
    {
        return await _dbContext.Positions
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Position?> UpdatePositionAsync(Position position)
    {
        _logger.LogInformation($"Trying to update position with id {position.Id}");

        try
        {
            Position? positionToUpdate = await _dbContext.Positions
                .FirstOrDefaultAsync(p => p.Id == position.Id);

            if (positionToUpdate != null)
            {
                _dbContext.Update(position);
                await _dbContext.SaveChangesAsync();

                return position;
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning($"Position wasn't updated: {ex.Message}");
        }

        return null;
    }

    public async Task<bool> IsPositionExistAsync(string name, int grade)
    {
        var position = await _dbContext.Positions.FirstOrDefaultAsync(p => 
            p.Name.Trim().ToUpper() == name.Trim().ToUpper() && p.Grade == grade);

        return position != null;
    }
}
