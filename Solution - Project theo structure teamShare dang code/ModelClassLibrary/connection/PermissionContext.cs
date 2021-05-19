using Microsoft.EntityFrameworkCore;
using ModelClassLibrary.area.auth;
using ModelClassLibrary.area.auth.model;
using ModelClassLibrary.area.auth.roles;
using ModelClassLibrary.area.model;

namespace ModelClassLibrary.connection
{
    public class PermissionContext : DbContext
    {
        public PermissionContext(DbContextOptions<PermissionContext> options) : base(options)
        {
        }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<GroupPermissions> GroupPermissions { get; set; }
        public DbSet<Groups> Groups { get; set; }
        public DbSet<UserPermissions> UserPermissions { get; set; }
        public DbSet<Permissions> Permissions { get; set; }
        public DbSet<PermissionTranslations> PermissionTranslations { get; set; }
        public DbSet<Languages> Languages { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserPermissions>().HasKey(i => new { i.usid, i.groupid });
            builder.Entity<GroupPermissions>().HasKey(i => new { i.groupid, i.perid });
        }
    }
}
