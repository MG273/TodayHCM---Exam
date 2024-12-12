namespace TodayHCM.Core.DTO;

public class ProductResponse
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public long? Price { get; set; }
}
public static class ProductExtensions
{
    public static ProductResponse ToProductResponse(this TeProduct product)
    {
        return new ProductResponse()
        {
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
        };
    }
}
