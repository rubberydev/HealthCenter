namespace HealthCenter.Backend.Models
{
    using Domain;

    public class Agenda : Scheduler
    {
        public virtual RegisterViewModel Medics { get; set; }
    }
}