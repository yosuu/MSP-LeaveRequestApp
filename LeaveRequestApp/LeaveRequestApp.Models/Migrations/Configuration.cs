namespace LeaveRequestApp.Models.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<LeaveRequestApp.Models.LeaveContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(LeaveRequestApp.Models.LeaveContext context)
        {
            if (System.Diagnostics.Debugger.IsAttached == false)
            {
                //System.Diagnostics.Debugger.Launch();
            }

            try
            {
                /* YEARS *******************************************************************/
                #region Years
                var years = new List<Years>
                {
                    new Years {ID = 1, Year = 2014 },
                    new Years {ID = 2, Year = 2015 },
                    new Years {ID = 3, Year = 2016 }
                };

                years.ForEach(y => context.Years.AddOrUpdate(x => x.Year, y));
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

                departments.ForEach(d => context.Departments.AddOrUpdate(x => x.Code, d));
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
                requestStatus.ForEach(r => context.RequestStatus.AddOrUpdate(x => x.Name, r));
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
                roles.ForEach(r => context.Roles.AddOrUpdate(x => x.Name, r));
                context.SaveChanges();
                #endregion

                /* EMPLOYEES ***************************************************************/
                #region employees
                var admin = new Employees
                {
                    Id = Guid.NewGuid().ToString(),
                    EmployeeID = "0001",
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
                    UserName = "0001"
                };
                context.Users.Add(admin);
                context.SaveChanges();
                #endregion

                /* EMPLOYEE ROLES **********************************************************/
                #region employee Roles
                var manager = new UserManager<Employees>(new UserStore<Employees>(context));
                manager.AddToRole(admin.Id, roles[0].Name);
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
