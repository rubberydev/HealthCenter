namespace HealthCenter.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SchedulerInfo
    {
        public int AgendaId { get; set; }
        public DateTime startHour { get; set; }
        public DateTime endHour { get; set; }
        public int idWorkDay { get; set; }
    }
}
