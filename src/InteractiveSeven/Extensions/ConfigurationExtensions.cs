﻿using InteractiveSeven.Twitch.Commands;
using InteractiveSeven.Twitch.Commands.Decorators;
using Microsoft.Extensions.DependencyInjection;

namespace InteractiveSeven
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection RegisterTwitchCommand<T>(this IServiceCollection services)
            where T : ITwitchCommand
        {
            return services.AddSingleton(typeof(T))
                .AddSingleton(typeof(ITwitchCommand), typeof(LoggingCommand<T>));
        }
    }
}