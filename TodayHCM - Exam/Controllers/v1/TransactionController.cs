namespace TodayHCM___Exam.Controllers.v1;

public class TransactionController(ITransactionAdderService transactionAdder,
    ITransactionGetterService transactionGetter) : TodayHCMControllerBase
{
    private readonly ITransactionAdderService _transactionAdderService = transactionAdder;
    private readonly ITransactionGetterService _transactionGetterService = transactionGetter;

    /// <summary>
    /// get all transaction in DB
    /// </summary>
    /// <returns>return a list of Transaction Model</returns>
    [HttpGet("Get-All-Transaction")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<TransactionResponse>>> GetAllTransaction()
    {
        var result = await _transactionGetterService.GetAllTransaction();

        if (result == null)
        {
            return NotFound();
        }

        return result;
    }
    
    /// <summary>
    /// you can search by ID
    /// </summary>
    /// <param name="id">ID of Transaction</param>
    /// <returns></returns>
    [HttpGet("Get-Transaction")]
    [Authorize]
    public async Task<ActionResult<TransactionResponse>> GetTransaction(int id)
    {
        var result = await _transactionGetterService.GetTransaction(id);

        if (result == null)
        {
            return NotFound();
        }

        return result;
    }

    /// <summary>
    /// you have storage and if some one buy a product form your store you must add transaction sell
    /// because a product leave your storage 
    /// </summary>
    /// <param name="UserID">ID of User</param>
    /// <param name="ProductID">ID of Product</param>
    /// <param name="Quantity">count of product</param>
    /// <param name="Time">and Time a customer buy from your store</param>
    /// <returns>return Ture if its successful</returns>
    [HttpPost("Add-Sell-Transaction")]
    [Authorize]
    public async Task<ActionResult<bool>> AddSellTransaction(int UserID, int ProductID, int Quantity, DateTime Time)
    {
        //Validation
        if (UserID == 0 || ProductID == 0 || Quantity == 0)
        {
            return Problem();
        }

        var result = await _transactionAdderService.TransactionProductSell(UserID, ProductID, Quantity, Time);

        if (result)
        {
            return Ok(result);
        }
        else
        {
            return Problem();
        }
    }

    /// <summary>
    /// if some one buy a product form your store you must add product to your storage again 
    /// </summary>
    /// <param name="UserID">ID of User</param>
    /// <param name="ProductID">ID of Product</param>
    /// <param name="Quantity">count of product</param>
    /// <param name="Time">and Time a customer buy from your store</param>
    /// <returns>return Ture if its successful</returns>
    [HttpPost("Add-Buy-Transaction")]
    [Authorize]
    public async Task<ActionResult<bool>> AddBuyTransaction(int UserID, int ProductID, int Quantity, DateTime Time)
    {
        //Validation
        if (UserID == 0 || ProductID == 0 || Quantity == 0)
        {
            return Problem();
        }

        var result = await _transactionAdderService.TransactionProductBuy(UserID, ProductID, Quantity, Time);

        if (result)
        {
            return Ok(result);
        }
        else
        {
            return Problem();
        }
    }

}
