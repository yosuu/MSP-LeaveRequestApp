using System;
using LeaveRequestApp.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LeaveRequestApp.Models
{
    public class MySqlInitializer : IDatabaseInitializer<LeaveContext>
    {
        public void InitializeDatabase(LeaveContext context)
        {
            if (!context.Database.Exists())
            {
                // if database did not exist before - create it
                context.Database.Create();
                GenerateData(context);
            }
            else
            {
                // query to check if MigrationHistory table is present in the database 
                var migrationHistoryTableExists = ((IObjectContextAdapter)context).ObjectContext.ExecuteStoreQuery<int>(
                string.Format(
                  "SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = '{0}' AND table_name = '__MigrationHistory'",
                  "leaverequest"));

                // if MigrationHistory table is not there (which is the case first time we run) - create it
                if (migrationHistoryTableExists.FirstOrDefault() == 0)
                {
                    context.Database.Delete();
                    context.Database.Create();
                    GenerateData(context);
                }
            }
        }

        private void GenerateData(LeaveContext context)
        {

            /* YEARS *******************************************************************/
            #region Years
            var years = new List<Years>
                {
                    new Years {ID = 1, Year = 2014 },
                    new Years {ID = 2, Year = 2015 },
                    new Years {ID = 3, Year = 2016 }
                };

            years.ForEach(y => context.Years.Add(y));
            context.SaveChanges();
            #endregion

            /* DEPARTMENTS *************************************************************/
            #region Departments
            var departments = new List<Departments>
                {
                    new Departments { ID = 1, Code = "IT", Name= "Information Technology" },
                    new Departments { ID = 2, Code = "HR", Name= "Human Resources" },
                    new Departments { ID = 3, Code = "SA", Name= "Sales" },
                    new Departments { ID = 4, Code = "PO", Name= "Production" },
                    new Departments { ID = 5, Code = "PR", Name= "Procurement" }
                };

            departments.ForEach(d => context.Departments.Add(d));
            context.SaveChanges();
            #endregion

            /* REQUEST STATUS **********************************************************/
            #region Request Status
            var requestStatus = new List<RequestStatus>
                {
                    new RequestStatus { ID = 1, Name = "Approved" },
                    new RequestStatus { ID = 2, Name = "Rejected" },
                    new RequestStatus { ID = 3, Name = "Pending" }
                };
            requestStatus.ForEach(r => context.RequestStatus.Add(r));
            context.SaveChanges();
            #endregion

            /* ROLES *******************************************************************/
            #region Roles
            var roles = new List<IdentityRole>
                {
                    new IdentityRole { Id = "7DB947B3-C49D-470B-98AC-636396F12786", Name = "Super Admin" },
                    new IdentityRole { Id = "9B6BE9B5-ED29-443E-AD4D-1E3BCD07B125", Name = "Admin" },
                    new IdentityRole { Id = "E3656F31-7FF5-4BF5-A5E6-1DDEDC4B0A1E", Name = "Approver" },
                    new IdentityRole { Id = "C3A33687-62FF-4E46-A896-824D18F31DF5", Name = "User" }
                };
            roles.ForEach(r => context.Roles.Add(r));
            context.SaveChanges();
            #endregion

            /* EMPLOYEES ***************************************************************/
            #region employees
            var employees = new List<Employees>
            {
                new Employees
                {
                    Id = Guid.NewGuid().ToString(),
                    EmployeeID = "0001",
                    FirstName = "Super",
                    LastName = "Administrator",
                    LeaveDayRemaining = 999,
                    Email = "",
                    EmailConfirmed = true,
                    PasswordHash = "AC3/mDvG6q17MHYugz3osCP1NTPzQDODNTlWbN8K5c6tNRran1hj8EJu2HcLkjyUUg==",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    PhoneNumberConfirmed = true,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                    UserName = "0001"
                },
                new Employees
                {
                    Id = Guid.NewGuid().ToString(),
                    EmployeeID = "0002",
                    FirstName = "Administrator",
                    LastName = "",
                    LeaveDayRemaining = 999,
                    Email = "",
                    EmailConfirmed = true,
                    PasswordHash = "AC3/mDvG6q17MHYugz3osCP1NTPzQDODNTlWbN8K5c6tNRran1hj8EJu2HcLkjyUUg==",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    PhoneNumberConfirmed = true,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                    UserName = "0002"
                },
                new Employees
                {
                    Id = Guid.NewGuid().ToString(),
                    EmployeeID = "0003",
                    FirstName = "Yoshua",
                    LastName = "Andrew",
                    LeaveDayRemaining = 999,
                    Email = "",
                    EmailConfirmed = true,
                    PasswordHash = "AC3/mDvG6q17MHYugz3osCP1NTPzQDODNTlWbN8K5c6tNRran1hj8EJu2HcLkjyUUg==",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    PhoneNumberConfirmed = true,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                    UserName = "0003"
                },
                new Employees
                {
                    Id = Guid.NewGuid().ToString(),
                    EmployeeID = "0004",
                    FirstName = "Yosi",
                    LastName = "Anggun",
                    LeaveDayRemaining = 999,
                    Email = "",
                    EmailConfirmed = true,
                    PasswordHash = "AC3/mDvG6q17MHYugz3osCP1NTPzQDODNTlWbN8K5c6tNRran1hj8EJu2HcLkjyUUg==",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    PhoneNumberConfirmed = true,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                    UserName = "0004"
                }
            };
            employees.ForEach(x => context.Users.Add(x));
            context.SaveChanges();
            #endregion

            /* EMPLOYEE ROLES **********************************************************/
            #region employee Roles
            var manager = new UserManager<Employees>(new UserStore<Employees>(context));
            manager.AddToRole(employees[0].Id, roles[0].Name);
            manager.AddToRole(employees[1].Id, roles[1].Name);
            manager.AddToRole(employees[2].Id, roles[2].Name);
            manager.AddToRole(employees[3].Id, roles[3].Name);
            #endregion

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
                    EmployeeID = employees[rdm.Next(1, 4)].EmployeeID,
                    year = year.ID,
                    BeginDate = new DateTime(year.Year, rdm.Next(1, 13), rdm.Next(1, 20)),
                    Reason = reason[rdm.Next(0, 2)],
                    LeaveDay = rdm.Next(1, 13),
                    RequestStatusID = requestStatus[rdm.Next(0, 3)].ID
                };
                Thread.Sleep(20);
                requests.Add(request);
            }

            requests.ForEach(x => context.Requests.Add(x));
            context.SaveChanges();

        }
    }
}