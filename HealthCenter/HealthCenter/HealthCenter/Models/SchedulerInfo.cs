

using System;

namespace HealthCenter.Models
{
    public class SchedulerInfo
    {
        public int AgendaId { get; set; }

        public DateTime startHour { get; set; }

        public DateTime endHour { get; set; }

        public DateTime DateToday { get; set; }

        public int idWorkDay { get; set; }

    }
}
