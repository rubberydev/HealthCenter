
namespace HealthCenter.Models
{
    using System;

    public class WorkDayList
    {
        public int idWorkDay { get; set; }

        public DateTime startDayHour { get; set; }

        public DateTime endDayHour { get; set; }

        public DateTime DateToday { get; set; }

        public int durationCite { get; set; }

        public WorkDayList()
        {

        }
    }
}
