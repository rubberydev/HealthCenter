
namespace HealthCenter.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    class Scheduler
    {
        public int AgendaId { get; set; }
        public DateTime startHour { get; set; }
        public DateTime endHour { get; set; }
        public int idWorkDay { get; set; }
        public WorkDay WorkDay { get; set; }
        public string ApplicationUser_Id { get; set; }
    }
}
