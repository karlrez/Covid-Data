using Microsoft.EntityFrameworkCore;
using assignment1.Entities;

namespace assignment1.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<CovidData> DailyCovidData { get; set; }
    }
}