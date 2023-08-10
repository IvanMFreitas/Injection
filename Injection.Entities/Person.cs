
namespace Injection.Entities;
public record Person : IEntity
{
    public string Name { get; init; }
    public string Email { get; init;}
    public bool IsAdmin { get; init;}

}
