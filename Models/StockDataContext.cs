using Microsoft.EntityFrameworkCore;

namespace FinanceWeb.Models
{
    public class StockDataContext : DbContext
    {
        public DbSet<Stock> Stocks { get; set; }

        public StockDataContext(DbContextOptions<StockDataContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
