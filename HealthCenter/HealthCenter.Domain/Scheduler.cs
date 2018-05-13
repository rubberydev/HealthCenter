namespace HealthCenter.Domain
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Scheduler
    {
        [Key]
        public int AgendaId { get; set; }

        [Display(Name = "start hour")]
        [DataType(DataType.Time)]
        [Required(ErrorMessage = "The field {0} is required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime startHour { get; set; }

        [Display(Name = "end hour")]
        [DataType(DataType.Time)]
        [Required(ErrorMessage = "The field {0} is required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime endHour { get; set; }

        [NotMapped]       
        public DateTime DateToday { get; set; }

        [Display(Name = "Date")]
        public int idWorkDay { set; get; }
        
        public int StateId { get; set; }

        [JsonIgnore]
        public virtual WorkDay WorkDay { set; get; }

        [JsonIgnore]
        public virtual State State { set; get; }        

        [Display(Name = "Doctor")]
        public string ApplicationUser_Id { get; set; }       

        
    }
}
