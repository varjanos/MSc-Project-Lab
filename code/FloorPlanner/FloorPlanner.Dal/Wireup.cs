using FloorPlanner.Dal.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;

namespace FloorPlanner.Dal;

public static class WireUp
{
    public static void AddDal(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<FloorPlannerDbContext>(options =>
            options.UseSqlServer(
                connectionString,
                opt => opt.MigrationsAssembly("FloorPlanner.Dal")
                    .MigrationsHistoryTable(HistoryRepository.DefaultTableName, "dbo")));
    }
}