namespace CleanVibe.Domain.Entities;

public class Product : BaseEntity<Guid>
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
}
