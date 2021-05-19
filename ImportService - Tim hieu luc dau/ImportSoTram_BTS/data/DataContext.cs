using Microsoft.EntityFrameworkCore;
using ModelClassLibrary.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImportSoTram_BTS.data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> op) : base(op)
        {
        }

        public DbSet<SoTramBST> SoTramBST { get; set; }
    }
}
