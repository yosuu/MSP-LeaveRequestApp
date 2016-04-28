using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaveRequestApp.Models
{
    public class Site
    {
        public static Site CurrentSite { get; set; }

        public string Url { get; set; }
    }
}