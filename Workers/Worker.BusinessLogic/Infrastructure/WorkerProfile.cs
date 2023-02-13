using AutoMapper;
using Worker.BusinessLogic.DTO;
using Worker.Data.Access.Entities;

namespace Worker.BusinessLogic.Infrastructure;

public class WorkerProfile : Profile
{
	public WorkerProfile()
	{
		CreateMap<Position, PositionDTO>().ReverseMap();
        CreateMap<Employee, EmployeeDTO>().ReverseMap();
    }
}
