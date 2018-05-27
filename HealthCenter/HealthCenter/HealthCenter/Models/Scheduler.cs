
namespace HealthCenter.Models
{
    using System;

    public class Scheduler
    {

        public int AgendaId
        {
            get;
            set;
        }

        public DateTime startHour
        {
            get;
            set;
        }

        public DateTime endHour
        {
            get;
            set;
        }

        public DateTime DateSchedule
        {
            get;
            set;
        }

        public int idWorkDay
        {
            get;
            set;
        }

        public int StateId
        {
            get;
            set;
        }

        public WorkDay WorkDay

        { get;
            set;
        }

        public State State
        {
            get;
            set;
        }

        public string ApplicationUser_Id
        {
            get;
            set;
        }

        public string NameDay
        {
            get;
            set;
        }

        public string DateScheduleS
        {
            get;
            set;
        }

        public string startHourS
        {
            get;
            set;
        }

    }
}
