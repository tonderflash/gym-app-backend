namespace MyGymApp.Domain.Common.BaseEntity;

public interface IBase
{
    public int Id { get; set; }
    public bool Deleted { get; set; }
}
public class BaseEntity : IBase
{
    public int Id { get; set; }
    public bool Deleted { get; set; }
}