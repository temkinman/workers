using Microsoft.EntityFrameworkCore;
using Worker.Data.Access.Entities;

namespace Worker.Data.Access.EF;

public class WorkerAppDbContext : DbContext
{
	public WorkerAppDbContext(DbContextOptions<WorkerAppDbContext> options) : base(options)
	{ }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>()
            .HasMany(e => e.Positions)
            .WithMany(e => e.Employees)
            .UsingEntity<EmployeePosition>(
                j => j
                    .HasOne(ep => ep.Position)
                    .WithMany(e => e.EmployeePositions)
                    .HasForeignKey(ep => ep.PositionId),
                j => j
                    .HasOne(ep => ep.Employee)
                    .WithMany(e => e.EmployeePositions)
                    .HasForeignKey(ep => ep.EmployeeId),
                j =>
                {
                    j.HasKey(t => new { t.EmployeeId, t.PositionId });
                });
    }

    public DbSet<Employee> Employees { get; set; }
	public DbSet<Position> Positions { get; set; }
	public DbSet<EmployeePosition> EmployeePositions { get; set; }

}
