using Worker.BusinessLogic.DTO;
using Worker.Data.Access.Entities;

namespace Worker.BusinessLogic.Interfaces;

public interface IEmployeeService
{
    Task<List<EmployeeDTO>> GetAllEmployeesAsync();
    Task<EmployeeDTO?> GetEmployeeByIdAsync(Guid id);
    Task<EmployeeDTO?> CreateEmployeeAsync(EmployeeDTO employeeDto);
    Task<EmployeeDTO?> UpdateEmployeeAsync(EmployeeDTO employeeDto);
    Task<bool> DeleteEmployeeAsync(Guid id);
    Task<List<EmployeeDTO>> GetEmployeesByPositionAsync(Guid positionId);
    Task<List<EmployeeDTO>> GetEmployeesByPositionAsync(string positionName);
}
