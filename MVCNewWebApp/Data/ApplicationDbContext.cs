using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCNewWebApp.Models;

namespace MVCNewWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<MVCNewWebApp.Models.Employee> Employee { get; set; }
        public DbSet<MVCNewWebApp.Models.Department> Department { get; set; }
        public DbSet<MVCNewWebApp.Models.Person> Person { get; set; }
        public DbSet<MVCNewWebApp.Models.Views.PersonAddressView> PersonAddressView { get; set; }
    }
}
