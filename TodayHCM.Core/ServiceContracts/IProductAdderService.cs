namespace TodayHCM.Core.ServiceContracts;

public interface IProductAdderService
{
    Task<ProductResponse> AddProduct(ProductAddRequest? productAddRequest);
}
