namespace TodayHCM.Core.ServiceContracts;

public interface ITransactionAdderService
{
    Task<bool> TransactionProductSell(int UserID, int ProductID, int Quantity, DateTime Time);
    Task<bool> TransactionProductBuy(int UserID, int ProductID, int Quantity, DateTime Time);
}
