using System.ComponentModel.DataAnnotations.Schema;

namespace Injection.Entities;
public record Product : IEntity
{
    public string Name { get; init; }
    [Column("Price", TypeName="Decimal(5,2)")]
    public decimal Price { get; init; }

}
