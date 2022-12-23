using Microsoft.EntityFrameworkCore;

namespace AutoSale.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Cars> Cars { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) 
        {
            
        }
    }
}
