using Microsoft.Extensions.DependencyInjection;
using PD.ChatHistory.Domain.Contracts.Repositories;
using PD.ChatHistory.Infrastructure.Contexts;
using PD.ChatHistory.Infrastructure.Repositories;
using PD.ChatHistory.Infrastructure.Seed;

namespace PD.ChatHistory.Infrastructure
{
    public static class DependencyInjectionInfrastructure
    {
        public static IServiceCollection SetupDI(IServiceCollection services)
        {
            services.AddDbContext<ChatHistoryDbContext>();

            DbSeeder.Seed(services);
            services.AddScoped<IChatRoomRepository, ChatRoomRepository>();
            return services;
        }
    }
}