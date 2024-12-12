namespace TodayHCM.Core.DTO;

public class ProductAddRequest
{
    [Required(ErrorMessage = "Name of Product can't be blank")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Description of Product can't be blank")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Price of Product can't be blank")]
    public long? Price { get; set; }

    /// <summary>
    /// Converts the current object of ProductAddRequest into a new object of TeProduct type
    /// </summary>
    /// <returns>return a new object of TeProduct type</returns>
    public TeProduct TeProduct() 
    {
        return new TeProduct { Name = Name, Description = Description, Price = Price };
    }
}
