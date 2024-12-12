namespace TodayHCM.Core.Services;

public class ProductGetterService : IProductGetterService
{
    private readonly IProductRepository _productRepository;
    public ProductGetterService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<List<ProductResponse>?> GetAllProduct()
    {
        var product = await _productRepository.GetAllProducts();

        return product.Select(temp =>temp.ToProductResponse()).ToList();
    }

    public async Task<ProductResponse?> GetProductByProductID(int productID)
    {
        if (productID == null)
            return null;

        TeProduct? product = await _productRepository.GetProductByProductID(productID);

        if (product == null) 
            return null;

        return product.ToProductResponse();
    }

    public async Task<ProductResponse?> GetProductByProductName(string productName)
    {
        if (productName == null)
            return null;

        TeProduct? product = await _productRepository.GetProductByProductName(productName);

        if (product == null)
            return null;

        return product.ToProductResponse();
    }

    public async Task<List<ProductResponse>?> GetFilteredProductByPrice(BigInteger startPrice, BigInteger endPrice)
    {
        var product = await _productRepository.GetFilteredProductsByPrice(startPrice, endPrice);
        if (product == null)
        {
            return null;
        }

        return product.Select(temp => temp.ToProductResponse()).ToList();
    }

    public async Task<int> GetCountProduct(int productID)
    {
        var count = await _productRepository.GetProductCountByProductID(productID);
        return count == 0 ? 0 : count;
    }
}
