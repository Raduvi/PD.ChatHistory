using Microsoft.Extensions.DependencyInjection;
using PD.ChatHistory.Application.Contracts.Services;
using PD.ChatHistory.Application.Profiles;
using PD.ChatHistory.Application.Services;

namespace PD.ChatHistory.Application
{
    public static class DependencyInjectionApplication
    {
        public static IServiceCollection SetupDI(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ChatHistoryMapProfile));
            services.AddScoped<IChatRoomHistoryService, ChatRoomHistoryService>();
            return services;
        }
    }
}