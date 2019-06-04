using InteractiveSeven.Core;
using InteractiveSeven.Core.Memory;
using InteractiveSeven.Core.Settings;
using InteractiveSeven.Core.ViewModels;
using InteractiveSeven.Twitch;
using InteractiveSeven.Twitch.Commands;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Windows;
using TwitchLib.Client;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static void InitializeSettings()
        {
            new SettingsStore().EnsureExists();
        }

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            InitializeSettings();
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient(typeof(IList<>), typeof(List<>));

            services.AddSingleton<MenuColorViewModel>();
            services.AddSingleton<NameBiddingViewModel>();
            services.AddSingleton<SettingsViewModel>();
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<IMemoryAccessor, MemoryAccessor>();
            services.AddSingleton<IMenuColorAccessor, MenuColorAccessor>();
            services.AddSingleton<INameAccessor, NameAccessor>();
            services.AddSingleton<ITwitchClient, TwitchClient>();

            services.AddSingleton<ITwitchCommand, MenuCommand>();
            services.AddSingleton<ITwitchCommand, NameCommand>();
            services.AddSingleton<ITwitchCommand, I7Command>();

            services.AddSingleton<TwitchSettings>();
            services.AddSingleton<IChatBot, ChatBot>();
            services.AddSingleton<ISettingsStore, SettingsStore>();
            services.AddSingleton<MainWindow>();
        }
    }
}
