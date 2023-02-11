using Microsoft.EntityFrameworkCore;
using Worker.Data.Access.Entities;

namespace Worker.Data.Access.EF;

public class WorkerAppDbContext : DbContext
{
	public WorkerAppDbContext(DbContextOptions<WorkerAppDbContext> options) : base(options)
	{ }

	public DbSet<Employee> Employees { get; set; }
	public DbSet<Position> Positions { get; set; }
}
