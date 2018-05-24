namespace HealthCenter.Domain
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class DataContext : DbContext
    {

        #region Properties
        public DbSet<User> Users { get; set; }

        public DbSet<UserType> UserTypes { get; set; }

        public DbSet<Scheduler> Schedulers { get; set; }

        public DbSet<WorkDay> WorkDays { get; set; }

        public DbSet<State> States { get; set; }

        public DbSet<UserSchedule> UserSchedules { get; set; }
        #endregion

        #region Constructor
        public DataContext() : base("DefaultConnection")
        {
        }
        #endregion

        #region Methods
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();            
        }
        #endregion
    }
}
