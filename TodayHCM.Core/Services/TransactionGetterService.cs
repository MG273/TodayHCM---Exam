namespace TodayHCM.Core.Services;

public class TransactionGetterService : ITransactionGetterService
{
    private readonly ITransactionsRepository _transactionsRepository;
    public TransactionGetterService(ITransactionsRepository repository)
    {
        _transactionsRepository = repository;
    }
    public async Task<List<TransactionResponse>?> GetAllTransaction()
    {
        var result = await _transactionsRepository.GetAllTransactions();
        return result.Select(temp => temp.ToTransactionResponse()).ToList();
    }

    public async Task<TransactionResponse?> GetTransaction(int id)
    {
        var result = await _transactionsRepository.GetTransactionByTransactionID(id);
        if (result == null)
            return null;
        return result.ToTransactionResponse();
    }
}