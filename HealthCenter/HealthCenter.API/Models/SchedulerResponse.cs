
namespace HealthCenter.API.Models
{
    using System;
    using Domain;

    public class SchedulerResponse
    { 
        public int AgendaId { get; set; }

        public DateTime startHour { get; set; }

        public DateTime endHour { get; set; }

        public int idWorkDay { set; get; }

        public  WorkDay WorkDay { set; get; }

        public  string ApplicationUser_Id { get; set; }
    }
}