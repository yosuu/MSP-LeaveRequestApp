using System;
using System.ComponentModel.DataAnnotations;

namespace LeaveRequestApp.Models
{
    public class Departments
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(4)]
        public string Code { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}