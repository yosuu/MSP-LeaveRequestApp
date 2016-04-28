using System;

namespace LeaveRequestApp.Models
{
    public class RequestViewModel
    {
        public int ID { get; set; }

        public int Year { get; set; }

        public string Requestor { get; set; }

        public DateTime BeginDate { get; set; }

        public int LeaveDay { get; set; }

        public string Reason { get; set; }

        public string RequestStatus { get; set; }
    }
}