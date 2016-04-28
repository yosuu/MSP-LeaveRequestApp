using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveRequestApp.Models
{
    public class Requests
    {
        public int ID { get; set; }

        [Required]
        [ForeignKey("Years")]
        public int year { get; set; }

        public virtual Years Years { get; set; }

        [Required]
        [MaxLength(128)]
        public string EmployeeID { get; set; }

        [Required]
        [Display(Name = "Begin Date")]
        public DateTime BeginDate { get; set; }

        [Required]
        [Display(Name = "Leave Duration (Day(s))")]
        public int LeaveDay { get; set; }

        [Required]
        [MaxLength(500)]
        public string Reason { get; set; }

        [Required]
        [ForeignKey("RequestStatus")]
        public int RequestStatusID { get; set; }

        public virtual RequestStatus RequestStatus { get; set; }
    }
}