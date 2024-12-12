namespace TodayHCM.Core.ServiceContracts;

public interface ITransactionGetterService
{
    Task<List<TransactionResponse>?> GetAllTransaction();
    Task<TransactionResponse?> GetTransaction(int id);
}
