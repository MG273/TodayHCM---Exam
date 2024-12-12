namespace TodayHCM.Infrastructure.DatabaseContext;

public partial class TodayHcmExamContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public TodayHcmExamContext()
    {
    }

    public TodayHcmExamContext(DbContextOptions<TodayHcmExamContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TeProduct> TeProducts { get; set; }

    public virtual DbSet<TeTransaction> TeTransactions { get; set; }

    public virtual DbSet<TeUser> TeUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-ODEI1SP\\MGDATA;Initial Catalog=TodayHCM_Exam;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TeProduct>(entity =>
        {
            entity.ToTable("TE_Product");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<TeTransaction>(entity =>
        {
            entity.ToTable("TE_Transaction");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BuyCount).HasColumnName("Buy_Count");
            entity.Property(e => e.ProductId).HasColumnName("Product_ID");
            entity.Property(e => e.SellCount).HasColumnName("Sell_Count");
            entity.Property(e => e.TransactionDate).HasColumnName("Transaction_Date");
            entity.Property(e => e.TransactionType).HasColumnName("Transaction_Type");
            entity.Property(e => e.UserId).HasColumnName("User_ID");

            entity.HasOne(d => d.Product).WithMany(p => p.TeTransactions)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TE_Transaction_TE_Product");

            entity.HasOne(d => d.User).WithMany(p => p.TeTransactions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TE_Transaction_TE_User");
        });

        modelBuilder.Entity<TeUser>(entity =>
        {
            entity.ToTable("TE_User");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Family).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


    public void sp_AddProduct(TeProduct product)
    {
        SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@Product_Name", product.Name),
                new SqlParameter("@Product_Description", product.Description),
                new SqlParameter("@Product_Price", product.Price)
            };
        Database.ExecuteSqlRaw("EXEC [dbo].[SP_Add_Product] @Product_Name,@Product_Description,@Product_Price",parameters);
    }

    public void sp_Product_Buy(int userID, int productID, int Quantity, DateTime dateTime) 
    {
        using (var command = Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = "[dbo].[SP_Product_Buy]";
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@UserID", userID));
            command.Parameters.Add(new SqlParameter("@ProductID", productID));
            command.Parameters.Add(new SqlParameter("@Quantity", Quantity));
            command.Parameters.Add(new SqlParameter("@DateTime", dateTime));

            Database.OpenConnection();
            command.ExecuteScalar();
        }
    }

    public void sp_Product_Sell(int userID, int productID, int Quantity, DateTime dateTime)
    {
        using (var command = Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = "[dbo].[SP_Product_Sell]";
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@UserID", userID));
            command.Parameters.Add(new SqlParameter("@ProductID", productID));
            command.Parameters.Add(new SqlParameter("@Quantity", Quantity));
            command.Parameters.Add(new SqlParameter("@DateTime", dateTime));

            Database.OpenConnection();
            command.ExecuteScalar();
        }
    }

    public List<TeProduct> sp_Fetch_Product_Filtered(BigInteger startPrice, BigInteger endPrice)
    {
        var startPriceDecimal = (decimal)startPrice;
        var endPriceDecimal = (decimal)endPrice;

        SqlParameter[] parameters = new SqlParameter[] {
        new SqlParameter("@StartPrice", startPriceDecimal),
        new SqlParameter("@EndPrice", endPriceDecimal)
        };

        return TeProducts
        .FromSqlRaw("EXEC [dbo].[SP_Fetch_Product_Filtered] @StartPrice, @EndPrice", parameters)
        .ToList();

    }

    public int sp_Calculate_Product_Count(int productID)
    {
        using (var command = Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = "[dbo].[SP_Calculate_Product_Count]";
            command.CommandType = CommandType.StoredProcedure;

            var productIdParam = new SqlParameter("@ProductID", productID);
            command.Parameters.Add(productIdParam);

            Database.OpenConnection();

            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return reader.GetInt32(0); // Read the first column as an integer
                }
            }
        }

        return 0;
    }
}
