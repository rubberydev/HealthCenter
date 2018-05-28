namespace HealthCenter.Models
{
    using Newtonsoft.Json;
    using System;

    public class Appointments
    {        
        
        public int AgendaId
        {
            get;
            set;
        }

        
        public DateTime StartHour
        {
            get;
            set;
        }

        
        public DateTime EndHour
        {
            get;
            set;
        }

        
        public DateTime DateSchedule
        {
            get;
            set;
        }

        
        public long IdWorkDay
        {
            get;
            set;
        }

        
        public long StateId
        {
            get;
            set;
        }

        public string DateSchedule_
        {
            get;
            set;
        }

        public string StartHour_
        {
            get;
            set;
        }

        public string NameDay
        {
            get;
            set;
        }
    }
}
