using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SharpPayStack.Models;
using SharpPayStack.Shared;

namespace SharpPayStack.Data;

public class DatabaseContext : IdentityDbContext<User>
{
    public DbSet<Customer> Customers = null!;
    public DbSet<BankDetails> BankDetails = null!;

    public DbSet<Transaction> Transactions = null!;

    public DbSet<Wallet> Wallets = null!;

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .Property(user => user.Role)
            .HasDefaultValue(Role.User)
            .HasConversion<string>();

        modelBuilder.Entity<Transaction>()
            .Property(tr => tr.Status)
            .HasDefaultValue(TransactionStatus.PENDING)
            .HasConversion<string>();

        // Configure Wallet Balance precision and default value
        modelBuilder.Entity<Wallet>()
            .Property(wallet => wallet.Balance)
            .HasPrecision(12, 2)
            .HasDefaultValue(0.00);
    }
}