namespace HealthCenter.Domain
{
    using System.Data.Entity;

    public class DataContext : DbContext
    {

        #region Properties
        public DbSet<User> Users { get; set; }

        public DbSet<UserType> UserTypes { get; set; } 
        #endregion

        #region Constructor
        public DataContext() : base("DefaultConnection")
        {
        }
        #endregion
    }
}
