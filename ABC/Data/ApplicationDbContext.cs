using ABC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace ABC.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<UserBank> UserBanks { get; set; }
        public DbSet<PaymentDetail> PaymentDetails { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .HasMany(u => u.UserBanks)
                .WithOne(ub => ub.User)
                .HasForeignKey(ub => ub.Uid);

            builder.Entity<Bank>()
                .HasMany(b => b.UserBanks)
                .WithOne(ub => ub.Bank)
                .HasForeignKey(ub => ub.BankId);

            builder.Entity<Transaction>()
                .HasOne(t => t.PaymentDetail)
                .WithMany()
                .HasForeignKey(t => t.PaymentDetailId);

            builder.Entity<Transaction>()
                .HasOne(t => t.Sender)
                .WithMany()
                .HasForeignKey(t => t.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Transaction>()
                .HasOne(t => t.Receiver)
                .WithMany()
                .HasForeignKey(t => t.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<PaymentDetail>()
               .HasOne(pd => pd.Bank)
               .WithMany()
               .HasForeignKey(pd => pd.BankId);

            //base.OnModelCreating(builder);
        }
    }
}
