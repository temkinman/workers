using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Xml.Linq;
using Worker.Data.Access.EF;
using Worker.Data.Access.Entities;
using Worker.Data.Access.Interfaces;

namespace Worker.Data.Access.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly WorkerAppDbContext _dbContext;
    private readonly ILogger<PositionRepository> _logger;

    public EmployeeRepository(WorkerAppDbContext dbContext, ILogger<PositionRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;

    }
    public async Task<Employee?> CreateEmployeeAsync(Employee employee)
    {
        _logger.LogInformation($"Trying to create employee with id {employee.Id}");

        await SetExistedPositions(employee);

        try
        {
            _dbContext.Employees.Add(employee);
            await _dbContext.SaveChangesAsync();
            return employee;
        }
        catch (Exception ex)
        {
            _logger.LogWarning($"Employee wasn't created: {ex.Message}");
            return null;
        }
    }

    public async Task<bool> DeleteEmployeeAsync(Guid id)
    {
        _logger.LogInformation($"Trying to delete employee with id {id}");

        try
        {
            Employee? employee = await _dbContext.Employees
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee != null && !employee.Positions.Any())
            {
                _dbContext.Remove(employee);
                await _dbContext.SaveChangesAsync();

                return true;
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning($"Employee wasn't deleted: {ex.Message}");
        }

        return false;
    }

    public async Task<List<Employee>> GetAllEmployeesAsync()
    {
        return await _dbContext.Employees
            .AsNoTracking()
            .Include(e => e.Positions)
            .ToListAsync();
    }

    public async Task<Employee?> GetEmployeeByIdAsync(Guid id)
    {
        return await _dbContext.Employees
            .AsNoTracking()
            .Include(e => e.Positions)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<List<Employee>> GetEmployeesByPositionAsync(Guid positionId)
    {
        return await _dbContext.EmployeePositions
            .AsNoTracking()
            .Where(e => e.PositionId == positionId)
            .Select(e => e.Employee)
            .ToListAsync();
    }

    public async Task<Employee?> UpdateEmployeeAsync(Employee employee)
    {
        _logger.LogInformation($"Trying to update employee with id {employee.Id}");

        try
        {
            Employee? employeeToUpdate = await _dbContext.Employees
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == employee.Id);

            if (employeeToUpdate != null)
            {
                _dbContext.Update(employee);
                await _dbContext.SaveChangesAsync();

                return employee;
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning($"Employee wasn't updated: {ex.Message}");
        }

        return null;
    }

    private async Task SetExistedPositions(Employee employee)
    {
        var allPositions = await _dbContext.Positions.ToListAsync();

        var result = employee.Positions
                    .Select(all => allPositions.FirstOrDefault(
                        pos => all.Name.Trim().ToUpper() == pos.Name.Trim().ToUpper() && all.Grade == pos.Grade, all));

        employee.Positions = result.ToList();
    }
}
