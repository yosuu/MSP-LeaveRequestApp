using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using LeaveRequestApp.DAL;
using LeaveRequestApp.Models;

namespace LeaveRequestApp.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            LeaveContext context = new LeaveContext();
            var years = new List<Years>
            {
                new Years {ID = 1, Year = 2014 },
                new Years {ID = 2, Year = 2015 },
                new Years {ID = 3, Year = 2016 }
            };

            var departments = new List<Departments>
            {
                new Departments { ID = 1, Code = "IT", Name= "Information Technology" },
                new Departments { ID = 2, Code = "HR", Name= "Human Resources" },
                new Departments { ID = 3, Code = "SA", Name= "Sales" },
                new Departments { ID = 4, Code = "PO", Name= "Production" },
                new Departments { ID = 5, Code = "PR", Name= "Procurement" }
            };

            var requestStatus = new List<RequestStatus>
            {
                new RequestStatus { ID = 1, Name = "Approved" },
                new RequestStatus { ID = 2, Name = "Rejected" },
                new RequestStatus { ID = 3, Name = "Pending" }
            };

            var employeeID = new List<string>
            {
                "0001",
                "0002",
                "0003",
                "0004"
            };

            var reason = new List<string>
            {
                "Vacation",
                "Sick",
                "Party"
            };

            var requests = new List<Requests>();

            for (int i = 0; i <= 100; i++)
            {
                Random rdm = new Random();
                var year = years[rdm.Next(0, 3)];
                Requests request = new Requests
                {
                    EmployeeID = employeeID[rdm.Next(1, 4)],
                    year = year.ID,
                    BeginDate = new DateTime(year.Year, rdm.Next(1, 13), rdm.Next(1, 20)),
                    Reason = reason[rdm.Next(0, 2)],
                    LeaveDay = rdm.Next(1, 13),
                    RequestStatusID = requestStatus[rdm.Next(0, 3)].ID
                };
                Console.WriteLine(request.EmployeeID);
                Console.WriteLine(request.year);
                Console.WriteLine(request.BeginDate);
                Console.WriteLine(request.Reason);
                Console.WriteLine(request.LeaveDay);
                Console.WriteLine(request.RequestStatusID);
                Console.WriteLine("**********************************************");
                Thread.Sleep(50);
            }

            requests.ForEach(x => context.Requests.Add(x));
            context.SaveChanges();
            Console.ReadLine();
        }
    }
}
