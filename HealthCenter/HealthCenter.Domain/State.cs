namespace HealthCenter.Domain
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class State
    {
        [Key]
        public int StateId { get; set; }

        [Display(Name = "State shedule")]
        [Required(ErrorMessage = "The field {0} is required")]
        public string stateName { get; set; }

        [JsonIgnore]
        public virtual ICollection<Scheduler> Schedulers { get; set; }
    }
}
