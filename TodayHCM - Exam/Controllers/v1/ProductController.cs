namespace TodayHCM___Exam.Controllers.v1;

public class ProductController(IProductAdderService adderService, IProductGetterService getterService) : TodayHCMControllerBase
{
    private readonly IProductAdderService _productAdderService = adderService;
    private readonly IProductGetterService _productGetterService = getterService;

    /// <summary>
    /// add a product with post request to this action method
    /// </summary>
    /// <param name="productAddRequest">all parameters are necessary</param>
    /// <returns>return object that you add to body if it save to DB</returns>
    [HttpPost("Add-Product")]
    [Authorize]
    public async Task<ActionResult<ProductResponse>> AddProduct(ProductAddRequest productAddRequest) 
    {
        //Validation
        if (ModelState.IsValid == false)
        {
            string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            return Problem(errorMessage);
        }

        var result = await _productAdderService.AddProduct(productAddRequest);

        if (result == null)
        {
            return Problem();
        }

        return result;
    }

    /// <summary>
    /// send get request to this action and get back all of product
    /// </summary>
    /// <returns>return a list of product</returns>
    [HttpGet("Get-All-Product")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAllProduct()
    {
        var result = await _productGetterService.GetAllProduct();

        if (result == null)
        {
            return NotFound();
        }

        return result;
    }

    /// <summary>
    /// send get request with ID in route string and get specific product
    /// </summary>
    /// <param name="id">ID of product</param>
    /// <returns>return an object of Product</returns>
    [HttpGet("Get-Product-Id")]
    [Authorize]
    public async Task<ActionResult<ProductResponse>> GetProductID(int id)
    {
        if(id <= 0) 
        {
            return BadRequest();
        }
        var result = await _productGetterService.GetProductByProductID(id);

        if (result == null)
        {
            return NotFound();
        }

        return result;
    }
    
    /// <summary>
    /// you can get product by name
    /// </summary>
    /// <param name="NameProduct">Name of Product</param>
    /// <returns>return an object of product</returns>
    [HttpGet("Get-Product-Name")]
    [Authorize]
    public async Task<ActionResult<ProductResponse>> GetProductName(string NameProduct)
    {
        ProductResponse? result;
        if (!string.IsNullOrEmpty(NameProduct))
        {
            result = await _productGetterService.GetProductByProductName(NameProduct);
        }
        else 
        {
            return BadRequest();        
        }
       

        if (result == null)
        {
            return NotFound();
        }

        return result;
    }
    
    /// <summary>
    /// you can Filter Product by price
    /// </summary>
    /// <param name="start">min Price</param>
    /// <param name="end">max Price</param>
    /// <returns>return a list of product</returns>
    [HttpGet("Get-Product-Filter")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<ProductResponse>>> GetFilteredProduct(BigInteger start, BigInteger end)
    {
        if (start >= end || end <= start || end <= 0) 
        { 
            return BadRequest(); 
        }
        var result = await _productGetterService.GetFilteredProductByPrice(start, end);

        if (result == null)
        {
            return NotFound();
        }

        return result;
    }
    
    /// <summary>
    /// and you can send request and get count of product
    /// </summary>
    /// <param name="id">ID of product</param>
    /// <returns>return Count</returns>
    [HttpGet("Get-Product-Count")]
    [Authorize]
    public async Task<ActionResult<int>> GetCountOfProduct(int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        var result = await _productGetterService.GetCountProduct(id);

        if (result == null)
        {
            return NotFound();
        }

        return result;
    }
}
