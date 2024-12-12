namespace TodayHCM.Infrastructure.Repositories;

public class TransactionRepository(TodayHcmExamContext _todayHcmExamContext) : ITransactionsRepository
{
    private readonly TodayHcmExamContext _context = _todayHcmExamContext;

    public async Task<TeTransaction> AddTransaction(TeTransaction transaction)
    {
        _context.TeTransactions.Add(transaction);
        return transaction;
    }

    public async Task AddTransactionBuy(int UserID, int ProductID, int Quantity, DateTime dateTime)
    {
        _context.sp_Product_Buy(UserID,ProductID,Quantity,dateTime);
    }

    public async Task AddTransactionSell(int UserID, int ProductID, int Quantity, DateTime dateTime)
    {
        _context.sp_Product_Sell(UserID,ProductID,Quantity,dateTime);
    }

    public async Task<List<TeTransaction>> GetAllTransactions()
    {
        return await _context.TeTransactions.ToListAsync();
    }

    public async Task<TeTransaction?> GetTransactionByTransactionID(int transactionID)
    {
        return await _context.TeTransactions.FirstOrDefaultAsync(temp => temp.Id == transactionID);
    }
}