namespace TodayHCM.Core.Domain.RepositoryContracts;

public interface IProductRepository
{
    /// <summary>
    /// add a product to db
    /// </summary>
    /// <param name="product">its an entity object</param>
    /// <returns>return product that saved</returns>
    Task<TeProduct> AddProduct(TeProduct product);

    /// <summary>
    /// get all of products
    /// </summary>
    /// <returns>return all products</returns>
    Task<List<TeProduct>> GetAllProducts();

    /// <summary>
    /// get product by id
    /// </summary>
    /// <param name="productID">id of product</param>
    /// <returns>return a product</returns>
    Task<TeProduct?> GetProductByProductID(int productID);

    /// <summary>
    /// get product by name
    /// </summary>
    /// <param name="productName">name of a product</param>
    /// <returns>return a product</returns>
    Task<TeProduct?> GetProductByProductName(string productName);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="start">start price</param>
    /// <param name="end">end price</param>
    /// <returns></returns>
    Task<List<TeProduct>?> GetFilteredProductsByPrice(BigInteger start, BigInteger end);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="productID"></param>
    /// <returns></returns>
    Task<int> GetProductCountByProductID(int productID);

}
