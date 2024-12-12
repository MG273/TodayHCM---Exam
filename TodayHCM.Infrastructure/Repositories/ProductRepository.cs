namespace TodayHCM.Infrastructure.Repositories;

public class ProductRepository(TodayHcmExamContext _todayHcmExamContext) : IProductRepository
{
    private readonly TodayHcmExamContext _context = _todayHcmExamContext;

    public async Task<TeProduct> AddProduct(TeProduct product)
    {
        _context.sp_AddProduct(product);
        return product;
    }

    public async Task<List<TeProduct>> GetAllProducts()
    {
        return await _context.TeProducts.ToListAsync();
    }

    public async Task<TeProduct?> GetProductByProductID(int productID)
    {
        return await _context.TeProducts.FirstOrDefaultAsync(temp => temp.Id == productID);
    }

    public async Task<TeProduct?> GetProductByProductName(string productName)
    {
        return await _context.TeProducts.FirstOrDefaultAsync(temp => temp.Name == productName);
    }

    public async Task<List<TeProduct>?> GetFilteredProductsByPrice(BigInteger start, BigInteger end)
    {
        return _context.sp_Fetch_Product_Filtered(start, end);
    }

    public async Task<int> GetProductCountByProductID(int productID)
    {
        return _context.sp_Calculate_Product_Count(productID);
    }
}