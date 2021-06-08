using Microsoft.Extensions.DependencyInjection;

namespace DevChatter.InteractiveGames.SharedUI
{
    public static class DependencyRegistrar
    {
        public static void AddSharedWindows(this IServiceCollection services)
        {
            services.AddSingleton<SettingsWindow>();
            services.AddSingleton<AccentStyleWindow>();
        }

    }
}
