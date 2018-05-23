
namespace HealthCenter.Models
{
  
    public class UserSchedule
    {
        public int UserScheduleId { get; set; }

        public int AgendaId { get; set; }

        public int UserId { get; set; }

        public virtual UserLocal User { get; set; }

        public virtual Scheduler Scheduler { get; set; }

    }
}
