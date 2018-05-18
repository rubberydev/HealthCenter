
namespace HealthCenter.API.Models
{
    using System;
    using Domain;

    public class SchedulerResponse
    { 
        public int AgendaId { get; set; }

        public DateTime startHour { get; set; }

        public DateTime endHour { get; set; }

        public DateTime DateSchedule { get; set; }

        public int idWorkDay { set; get; }

        public int StateId { get; set; }

        public  WorkDay WorkDay { set; get; }

        public  State State { set; get; }

        public  string ApplicationUser_Id { get; set; }
    }
}