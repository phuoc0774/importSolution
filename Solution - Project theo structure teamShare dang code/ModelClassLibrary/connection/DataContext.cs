using Microsoft.EntityFrameworkCore;
using ModelClassLibrary.area.auth;
using ModelClassLibrary.area.model;
using ModelClassLibrary.model;

namespace ModelClassLibrary.connection
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<SalaryProcess>().HasKey(i => new { i.storeid, i.processid });
        }


        public DbSet<SoTramBTS> SoTramBTS { get; set; }
        public DbSet<KhoaSoTramBTS> KhoaSoTramBTS { get; set; }

    }
    
}
