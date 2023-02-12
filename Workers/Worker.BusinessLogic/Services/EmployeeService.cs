﻿using AutoMapper;
using Worker.BusinessLogic.DTO;
using Worker.BusinessLogic.Interfaces;
using Worker.Data.Access.Entities;
using Worker.Data.Access.Interfaces;

namespace Worker.BusinessLogic.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
        _mapper = InitializeAutomapper();
    }

    public async Task<EmployeeDTO?> CreateEmployeeAsync(EmployeeDTO employeeDto)
    {
        if (employeeDto != null)
        {
            Employee employee = _mapper.Map<EmployeeDTO, Employee>(employeeDto);

            var result = await _employeeRepository.CreateEmployeeAsync(employee);

            if(result == null)
            {
                return null;
            }

            return employeeDto;
        }

        return null;
    }

    public async Task DeleteEmployeeAsync(Guid id)
    {
        await _employeeRepository.DeleteEmployeeAsync(id);
    }

    public async Task<List<EmployeeDTO>> GetAllEmployeesAsync()
    {
        List<Employee> employees = await _employeeRepository.GetAllEmployeesAsync();

        return _mapper.Map<List<Employee>, List<EmployeeDTO>>(employees);
    }

    public async Task<EmployeeDTO?> GetEmployeeByIdAsync(Guid id)
    {
        Employee? employee = await _employeeRepository.GetEmployeeByIdAsync(id);

        if(employee == null)
        {
            return null;
        }

        return _mapper.Map<Employee, EmployeeDTO>(employee);
    }

    public async Task<List<EmployeeDTO>> GetEmployeesByPositionAsync(Guid positionId)
    {
        List<Employee> employees = await _employeeRepository.GetEmployeesByPositionAsync(positionId);

        return _mapper.Map<List<Employee>, List<EmployeeDTO>>(employees);
    }

    public async Task<EmployeeDTO?> UpdateEmployeeAsync(EmployeeDTO employeeDto)
    {
        if (employeeDto != null)
        {
            Employee employee = _mapper.Map<EmployeeDTO, Employee>(employeeDto);

            var result = await _employeeRepository.UpdateEmployeeAsync(employee);

            if(result == null)
            {
                return null;
            }

            return employeeDto;
        }

        return null;
    }

    private Mapper InitializeAutomapper()
    {
        var config = new MapperConfiguration(cfg =>
            cfg.CreateMap<Employee, EmployeeDTO>()
            .ReverseMap()
        );

        return new Mapper(config);
    }
}
