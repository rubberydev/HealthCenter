﻿
namespace HealthCenter.API.Models
{
    using System;

    public class WorkDayResponse
    {
     
        public int idWorkDay { set; get; }

     
        public DateTime startDayHour { set; get; }

  
        public DateTime endDayHour { set; get; }

        
        public DateTime DateToday { get; set; }

        
        public int durationCite { set; get; }

      }
}