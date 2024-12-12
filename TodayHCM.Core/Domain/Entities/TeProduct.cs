namespace TodayHCM.Core.Domain.Entities;

public partial class TeProduct
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public long? Price { get; set; }

    public virtual ICollection<TeTransaction> TeTransactions { get; set; } = new List<TeTransaction>();
}
