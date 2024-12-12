namespace TodayHCM.Core.Domain.Entities;

public partial class TeTransaction
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int ProductId { get; set; }

    public DateTime TransactionDate { get; set; }

    public int BuyCount { get; set; }

    public int SellCount { get; set; }

    public bool TransactionType { get; set; }

    public virtual TeProduct Product { get; set; } = null!;

    public virtual TeUser User { get; set; } = null!;
}
