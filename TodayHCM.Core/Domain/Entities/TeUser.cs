namespace TodayHCM.Core.Domain.Entities;

public partial class TeUser
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Family { get; set; }

    public string UserName { get; set; } = null!;

    public string? Email { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<TeTransaction> TeTransactions { get; set; } = new List<TeTransaction>();
}
