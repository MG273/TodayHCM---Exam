namespace TodayHCM.Core.ServiceContracts;

public interface IProductGetterService 
{
    Task<List<ProductResponse>?> GetAllProduct();
    Task<ProductResponse?> GetProductByProductID(int productID);
    Task<ProductResponse?> GetProductByProductName(string productName);
    Task<List<ProductResponse>?> GetFilteredProductByPrice(BigInteger startPrice, BigInteger endPrice);
    Task<int> GetCountProduct(int productID);
}
