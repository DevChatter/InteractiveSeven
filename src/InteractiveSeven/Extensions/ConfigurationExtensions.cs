using InteractiveSeven.Core.Commands;
using InteractiveSeven.Core.Commands.Decorators;
using InteractiveSeven.Core.Data.Items;
using Microsoft.Extensions.DependencyInjection;

namespace InteractiveSeven
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection RegisterTwitchCommand<T>(this IServiceCollection services)
            where T : IChatCommand
        {
            return services.AddSingleton(typeof(T))
                .AddSingleton(typeof(IChatCommand), typeof(LoggingCommand<T>));
        }

        public static IServiceCollection RegisterBattleCommand<T>(this IServiceCollection services)
            where T : IChatCommand
        {
            return services.AddSingleton(typeof(T))
                .AddSingleton(typeof(BattleOnlyCommand<T>))
                .AddSingleton(typeof(IChatCommand), typeof(LoggingCommand<BattleOnlyCommand<T>>));
        }

        public static IServiceCollection RegisterNonBattleCommand<T>(this IServiceCollection services)
            where T : IChatCommand
        {
            return services.AddSingleton(typeof(T))
                .AddSingleton(typeof(NonBattleCommand<T>))
                .AddSingleton(typeof(IChatCommand), typeof(LoggingCommand<NonBattleCommand<T>>));
        }

        public static IServiceCollection RegisterEquipmentData(this IServiceCollection services)
        {
            return services.AddSingleton<EquipmentData<Weapon>>()
                .AddSingleton<EquipmentData<Armlet>>()
                .AddSingleton<EquipmentData<Accessory>>();
        }
    }
}
