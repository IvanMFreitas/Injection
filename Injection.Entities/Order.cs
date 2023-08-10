using System.ComponentModel.DataAnnotations.Schema;

namespace Injection.Entities;
public record Order : IEntity
{
    [ForeignKey("Person")]
    public Guid PersonId { get; set; }
    public virtual Person Person { get; init;}
    
    [ForeignKey("Product")]
    public Guid ProductId { get; set; }
    public virtual Product Product { get; init;}
    public int Qty { get; set; }
    [Column("Total", TypeName="Decimal(5,2)")]
    public decimal Total { get; set; }
}
