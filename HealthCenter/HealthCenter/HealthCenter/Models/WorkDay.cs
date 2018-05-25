namespace HealthCenter.Models
{
    using System;
    using System.Collections.Generic;

    public class WorkDay
    {
        public int idWorkDay { get; set; }

        public DateTime startDayHour { get; set; }

        public DateTime endDayHour { get; set; }

        public DateTime DateToday { get; set; }

        public int durationCite { get; set; }

        public object parameterWorkDays { get; set; }
    }
}

