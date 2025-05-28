using IronCore.Domain.Entities.Product;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class CartDbModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int? UserId { get; set; }
    public string SessionId { get; set; } 
    public virtual ICollection<ProductDbModel> ProductsInCart { get; set; } = new List<ProductDbModel>();
    public decimal Price { get; set; } = 0;
    public decimal Discount { get; set; } = 0;
}
