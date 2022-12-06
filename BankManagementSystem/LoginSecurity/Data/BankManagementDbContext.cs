using LoginSecurity.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace LoginSecurity.Data
{
    public class BankManagementDbContext : DbContext
    {
        public BankManagementDbContext(DbContextOptions<BankManagementDbContext> options) : base(options)
        {

        }

        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<LoanDetail> LoanDetails { get; set; }

    }
}
