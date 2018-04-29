namespace HealthCenter.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Scheduler
    {
        [Key]
        public int AgendaId { get; set; }

        [Display(Name = "start hour")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime startHour { get; set; }

        [Display(Name = "start hour")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime endHour { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{00:yyyy/MM/dd}")]
        public DateTime DateToday { get; set; }

        //[Display(Name = "Doctor")]
        //public int IdMedic { get; set; }

    }
}
