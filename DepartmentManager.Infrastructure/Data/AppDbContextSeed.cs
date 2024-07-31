using DepartmentManager.Domain.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentManager.Infrastructure.Data
{
    public class AppDbContextSeed
    {
        public static async Task SeedAsync(AppDbContext context, ILogger<AppDbContextSeed> logger)
        {
            try
            {
                if (!await context.Departments.AnyAsync())
                {
                    await context.Departments.AddRangeAsync(
                        new Department { Name = "Executive" },
                        new Department { Name = "Human Resources" },
                        new Department { Name = "Finance" },
                        new Department { Name = "Information Technology" },
                        new Department { Name = "Marketing" }
                    );

                    await context.SaveChangesAsync();
                    logger.LogInformation("Seed database associated with context {DbContextName}", typeof(AppDbContext).Name);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding the database.");
            }
        }
    }
}
