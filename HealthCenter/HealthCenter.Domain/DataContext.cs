namespace HealthCenter.Domain
{
    using System.Data.Entity;

    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {

        }

        public System.Data.Entity.DbSet<HealthCenter.Domain.User> Users { get; set; }

        public System.Data.Entity.DbSet<HealthCenter.Domain.UserType> UserTypes { get; set; }
    }
}
