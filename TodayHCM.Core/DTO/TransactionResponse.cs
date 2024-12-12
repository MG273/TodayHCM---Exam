namespace TodayHCM.Core.DTO;

public class TransactionResponse
{
    public int? UserID { get; set; }
    public int? ProductID { get; set; }
    public DateTime? TransactionDate { get; set; }
    public bool TransactionType { get; set; }
    public int Count { get; set; }
}
public static class TransactionExtensions
{
    public static TransactionResponse ToTransactionResponse(this TeTransaction transaction)
    {
        int ct = 0;
        if (transaction.TransactionType)
        {
            ct = transaction.SellCount;
        }
        else 
        {
            ct = transaction.BuyCount;
        }
        return new TransactionResponse()
        {
            UserID = transaction.UserId,
            ProductID = transaction.ProductId,
            TransactionDate = transaction.TransactionDate,
            TransactionType = transaction.TransactionType,
            Count = ct
        };
    }
}