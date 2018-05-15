namespace HealthCenter.Domain
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public class WorkDay
    {
        [Key]
        public int idWorkDay { set; get; }

        [Display(Name = "start day hour")]
        [DataType(DataType.Time)]
        [Required(ErrorMessage = "The field {0} is required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime startDayHour { set; get; }

        [Display(Name = "end day hour")]
        [DataType(DataType.Time)]
        [Required(ErrorMessage = "The field {0} is required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime endDayHour { set; get; }

        [Display(Name = "Date Agenda start")]        
        [Required(ErrorMessage = "The field {0} is required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{00:yyyy/MM/dd}")]
        public DateTime DateToday { get; set; }

        [Display(Name = "Duration Cite")]
        public int durationCite { set; get; }

        [NotMapped]
        [Display(Name = "Schedule work days for")]
        public string parameterWorkDays { get; set; }

        public virtual ICollection<Scheduler> Scheduler { set; get; }
    }
}
