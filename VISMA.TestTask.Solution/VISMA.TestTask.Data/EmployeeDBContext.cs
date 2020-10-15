using Microsoft.EntityFrameworkCore;
using VISMA.TestTask.Data.Models;

namespace VISMA.TestTask.Data
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options)
            : base(options)
        { }

        public DbSet<Employee> Employee { get; set; }
    }
}
