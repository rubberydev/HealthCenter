using HealthCenter.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthCenter.Backend.Models
{
    public class WorkDaySchedule : WorkDay
    {
        public virtual ICollection<Agenda> Agendas { set; get; }
    }
}