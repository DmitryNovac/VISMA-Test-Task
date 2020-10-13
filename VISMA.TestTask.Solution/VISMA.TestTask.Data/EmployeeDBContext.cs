using System.Collections.Generic;
using System.Linq;
using VISMA.TestTask.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace VISMA.TestTask.Data
{
    public class EmployeeDBContext : DbContext, IEmployeeDBContext
    {
        public EmployeeDBContext(DbContextOptions<EmployeeDBContext> options)
            : base(options)
        { }

        public DbSet<Employer> Employee { get; set; }
    }
}
