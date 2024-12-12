namespace TodayHCM.Core.Services;

public class TransactionAdderService : ITransactionAdderService
{
    private readonly ITransactionsRepository _transactionsRepository;
    public TransactionAdderService(ITransactionsRepository repository)
    {
        _transactionsRepository = repository;
    }
    public async Task<bool> TransactionProductBuy(int UserID, int ProductID, int Quantity, DateTime Time)
    {
        try {
            await _transactionsRepository.AddTransactionBuy(UserID, ProductID, Quantity, Time);
            return true;
        }
        catch(Exception) { 
            return false;
        }        
    }

    public async Task<bool> TransactionProductSell(int UserID, int ProductID, int Quantity, DateTime Time)
    {
        try
        {
            await _transactionsRepository.AddTransactionSell(UserID, ProductID, Quantity, Time);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
