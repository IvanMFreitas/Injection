using System.ComponentModel.DataAnnotations;

namespace Injection.Entities;
public record IEntity
{
    [Key]
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }

}
