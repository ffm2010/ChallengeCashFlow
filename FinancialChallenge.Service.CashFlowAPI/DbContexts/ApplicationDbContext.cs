using FinancialChallenge.Service.CashFlowAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancialChallenge.Service.CashFlowAPI.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Transaction> Transctions { get; set; }
    }
}
