using Worker.Data.Access.Entities;

namespace Worker.Data.Access.Interfaces;

public interface IEmployeeRepository
{
    Task<List<Employee>> GetAllEmployeesAsync();
    Task<Employee?> GetEmployeeByIdAsync(Guid id);
    Task<Employee?> CreateEmployeeAsync(Employee employee);
    Task<Employee?> UpdateEmployeeAsync(Employee employee);
    Task<bool> DeleteEmployeeAsync(Guid id);
    Task<List<Employee>> GetEmployeesByPositionAsync(Guid positionId);
    Task<List<Employee>> GetEmployeesByPositionAsync(string positionName);
}
