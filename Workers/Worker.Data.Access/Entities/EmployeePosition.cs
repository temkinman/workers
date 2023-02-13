namespace Worker.Data.Access.Entities;

public class EmployeePosition
{
    public Guid EmployeeId { get; set; }
    public virtual Employee Employee { get; set; }
    public Guid PositionId { get; set; }
    public virtual Position Position { get; set; }
}
