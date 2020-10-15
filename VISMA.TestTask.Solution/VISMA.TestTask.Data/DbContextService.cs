using Microsoft.EntityFrameworkCore;
using VISMA.TestTask.Data.Models;

namespace VISMA.TestTask.Data
{
    public class DbContextService : IEmployeeDbContext
    {
        private readonly EmployeeDbContext DbContext;

        public DbSet<Employee> Employee
        {
            get => DbContext.Employee;
            set => DbContext.Employee = value;
        }

        public DbContextService()
        {
            var options = new DbContextOptionsBuilder<EmployeeDbContext>()
                .UseInMemoryDatabase(databaseName: "EmployeeDb")
                .Options;

            DbContext = new EmployeeDbContext(options);
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }

        public int SaveChanges()
        {
            return DbContext.SaveChanges();
        }
    }
}
