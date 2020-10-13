using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using VISMA.TestTask.Data.Models;

namespace VISMA.TestTask.Data
{
    public interface IEmployeeDBContext
    {
        DbSet<Employer> Employee { get; set; }

        int SaveChanges();
    }
}
