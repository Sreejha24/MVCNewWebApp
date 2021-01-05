using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCNewWebApp.Data
{
    public class MVCNewWebAppContext : DbContext
    {
        public MVCNewWebAppContext(DbContextOptions<MVCNewWebAppContext> options)
            : base(options)
        {
        }
        public DbSet<MVCNewWebApp.Models.Person> Person { get; set; }
        public DbSet<MVCNewWebApp.Models.Employee> Employee { get; set; }
        public DbSet<MVCNewWebApp.Models.Department> Department { get; set; }
        public DbSet<MVCNewWebApp.Models.Views.PersonAddressView> PersonAddressViews { get; set; }
    }
}
