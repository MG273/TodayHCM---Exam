namespace TodayHCM.Core.Services;

public class ProductAdderService : IProductAdderService
{
    private readonly IProductRepository _productRepository;

    public ProductAdderService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductResponse> AddProduct(ProductAddRequest? productAddRequest)
    {
        if (productAddRequest == null) 
        {
            throw new ArgumentNullException(nameof(productAddRequest));
        }

        ValidationHelper.ModelValidation(productAddRequest);

        TeProduct product = productAddRequest.TeProduct();

        await _productRepository.AddProduct(product);

        return product.ToProductResponse();
    }
}
