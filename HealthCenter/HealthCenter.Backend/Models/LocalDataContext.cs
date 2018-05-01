namespace HealthCenter.Backend.Models
{
    using Domain;

    public class LocalDataContext : DataContext
    {       
        public System.Data.Entity.DbSet<HealthCenter.Backend.Models.Agenda> Agenda { get; set; }

        public System.Data.Entity.DbSet<HealthCenter.Backend.Models.WorkDaySchedule> WorkDays { get; set; }
    }
}