namespace TodayHCM.Core.Domain.RepositoryContracts;

public interface ITransactionsRepository
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="teTransaction"></param>
    /// <returns></returns>
    Task<TeTransaction> AddTransaction(TeTransaction teTransaction);

    /// <summary>
    /// add a transaction to buy a product for storage
    /// </summary>
    /// <param name="UserID"></param>
    /// <param name="ProductID"></param>
    /// <param name="Quantity"></param>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    Task AddTransactionBuy(int UserID, int ProductID, int Quantity, DateTime dateTime);

    /// <summary>
    /// sell a product to a customer and add this transaction to DB
    /// </summary>
    /// <param name="UserID"></param>
    /// <param name="ProductID"></param>
    /// <param name="Quantity"></param>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    Task AddTransactionSell(int UserID, int ProductID, int Quantity, DateTime dateTime);

    /// <summary>
    /// get all of transaction in DB
    /// </summary>
    /// <returns>return all transactions</returns>
    Task<List<TeTransaction>> GetAllTransactions();

    /// <summary>
    /// get transaction by id
    /// </summary>
    /// <param name="transactionID">search by ID</param>
    /// <returns>return match ID in transactions List</returns>
    Task<TeTransaction?> GetTransactionByTransactionID(int transactionID);
}
