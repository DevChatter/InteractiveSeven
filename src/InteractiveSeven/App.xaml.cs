using InteractiveSeven.Core.Settings;
using InteractiveSeven.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace InteractiveSeven
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider _serviceProvider;

        public App()
        {
            InitializeSettings();
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<MenuColorViewModel>();
            services.AddSingleton<SettingsViewModel>();
            services.AddSingleton<ISettingsStore, SettingsStore>();
            services.AddSingleton<MainWindow>();
        }

        private static void InitializeSettings()
        {
            new SettingsStore().EnsureExists();
        }

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}
