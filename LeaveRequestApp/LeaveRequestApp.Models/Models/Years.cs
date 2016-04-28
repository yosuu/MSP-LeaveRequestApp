using System;
using System.ComponentModel.DataAnnotations;

namespace LeaveRequestApp.Models
{
    public class Years
    {
        public int ID { get; set; }

        [Required]
        public int Year { get; set; }
    }
}