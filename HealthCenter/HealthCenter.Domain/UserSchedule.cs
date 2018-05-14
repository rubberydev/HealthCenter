namespace HealthCenter.Domain
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;

    public class UserSchedule
    {
        [Key]
        public int UserScheduleId { get; set; }

        public int AgendaId { get; set; }

        public int UserId { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }

        [JsonIgnore]
        public virtual Scheduler Scheduler { get; set; }

    }
}
