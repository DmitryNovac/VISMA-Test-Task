using Microsoft.EntityFrameworkCore;
using System;
using VISMA.TestTask.Data.Models;

namespace VISMA.TestTask.Data
{
    public interface IEmployeeDbContext : IDisposable
    {
        DbSet<Employee> Employee { get; set; }

        int SaveChanges();
    }
}
