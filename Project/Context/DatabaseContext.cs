using JWT.Models;
using Microsoft.EntityFrameworkCore;

namespace JWT.Context;

public class DatabaseContext : DbContext
{
    
    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }
    
    public DbSet<Client> Clients { get; set; }
    public DbSet<IndividualClient> IndividualClients { get; set; }
    public DbSet<CompanyClient> CompanyClients { get; set; }
    public DbSet<Software> Softwares { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<Contract> Contracts { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Employee> Employees { get; set; }
    
     protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>()
            .HasDiscriminator<string>("ClientType")
            .HasValue<IndividualClient>("Individual")
            .HasValue<CompanyClient>("Company");
        
        modelBuilder.Entity<Client>()
            .HasQueryFilter(c => !c.IsSoftDeleted);

        modelBuilder.Entity<Client>()
            .HasMany(c => c.Contracts)
            .WithOne(c => c.Client)
            .HasForeignKey(c => c.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Client>()
            .HasMany(c => c.Payments)
            .WithOne(p => p.Client)
            .HasForeignKey(p => p.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Contract>()
            .HasOne(c => c.Client)
            .WithMany(c => c.Contracts)
            .HasForeignKey(c => c.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Contract>()
            .HasOne(c => c.Software)
            .WithMany(s => s.Contracts)
            .HasForeignKey(c => c.SoftwareId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Contract>()
            .Property(c => c.IsSigned)
            .HasDefaultValue(false);

        modelBuilder.Entity<Payment>()
            .HasOne(p => p.Client)
            .WithMany(c => c.Payments)
            .HasForeignKey(p => p.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Payment>()
            .HasOne(p => p.Contract)
            .WithMany(c => c.Payments)
            .HasForeignKey(p => p.ContractId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Discount>()
            .HasMany(d => d.Softwares)
            .WithMany(s => s.Discounts)
            .UsingEntity<Dictionary<string, object>>(
                "SoftwareDiscount",
                j => j.HasOne<Software>().WithMany().HasForeignKey("SoftwareId"),
                j => j.HasOne<Discount>().WithMany().HasForeignKey("DiscountId"));

        modelBuilder.Entity<IndividualClient>()
            .Property(c => c.FirstName)
            .IsRequired();

        modelBuilder.Entity<IndividualClient>()
            .Property(c => c.LastName)
            .IsRequired();

        modelBuilder.Entity<IndividualClient>()
            .Property(c => c.PESEL)
            .IsRequired();

        modelBuilder.Entity<CompanyClient>()
            .Property(c => c.CompanyName)
            .IsRequired();

        modelBuilder.Entity<CompanyClient>()
            .Property(c => c.KRS)
            .IsRequired();

        base.OnModelCreating(modelBuilder);
    }
}