using FocusXp.Domain.Exceptions;
using FocusXp.Infrastructure.Data;
using FocusXp.Infrastructure.Interfaces.Data;
using FocusXp.Infrastructure.Interfaces.Repository;
using FocusXp.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FocusXp.Infrastructure.Extensions;

public static class ConfigureInfrastructure
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureDatabase(configuration);
        services.ConfigureRepositories();
        services.ConfigureUnitOfWork();
    }

    private static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<FocusXpContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionStringByName("FocusXp"));
        });
    }

    private static string GetConnectionStringByName(this IConfiguration configuration, string name)
    {
        var connectionString = configuration.GetConnectionString(name);

        if (string.IsNullOrEmpty(connectionString))
            throw new ConnectionStringNullException(name);

        return connectionString;
    }

    private static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IAttachmentRepository, AttachmentRepository>();
        services.AddScoped<IBadgeRepository, BadgeRepository>();
        services.AddScoped<IPomodoroDistractionRepository, PomodoroDistractionRepository>();
        services.AddScoped<IPomodoroSessionRepository, PomodoroSessionRepository>();
        services.AddScoped<ISubWorkItemRepository, SubWorkItemRepository>();
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<IUserBadgeRepository, UserBadgeRepository>();
        services.AddScoped<IUserDailyStatisticRepository, UserDailyStatisticRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserSettingRepository, UserSettingRepository>();
        services.AddScoped<IWorkItemRepository, WorkItemRepository>();
        services.AddScoped<IWorkItemTagRepository, WorkItemTagRepository>();
        services.AddScoped<IXpTransactionRepository, XpTransactionRepository>();
    }

    private static void ConfigureUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}