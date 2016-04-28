using System;
using System.ComponentModel.DataAnnotations;

namespace LeaveRequestApp.Models
{
    public class RequestStatus
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
    }
}