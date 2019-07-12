using InteractiveSeven.Core;
using InteractiveSeven.Core.Bidding.Naming;
using InteractiveSeven.Core.Data;
using InteractiveSeven.Core.IntervalMessages;
using InteractiveSeven.Core.Memory;
using InteractiveSeven.Core.Models;
using InteractiveSeven.Core.Services;
using InteractiveSeven.Core.Settings;
using InteractiveSeven.Core.ViewModels;
using InteractiveSeven.Services;
using InteractiveSeven.Twitch;
using InteractiveSeven.Twitch.Commands;
using InteractiveSeven.Twitch.IntervalMessages;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using InteractiveSeven.Core.Workloads;
using InteractiveSeven.Startup;
using TwitchLib.Client;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private WorkloadCoordinator _workloadCoordinator;

        private static void InitializeSettings()
        {
            new SettingsStore().EnsureExists();
        }

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            InitializeSettings();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File("logs\\i7log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var dataLoader = serviceProvider.GetService<DataLoader>();
            dataLoader.LoadPreviousData();

            _workloadCoordinator = serviceProvider.GetService<WorkloadCoordinator>();

            var mainWindow = serviceProvider.GetService<MainWindow>();

            mainWindow.Show();
        }


        private void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient(typeof(IList<>), typeof(List<>));

            services.AddSingleton<WorkloadCoordinator>();

            services.AddSingleton<MenuColorViewModel>();
            services.AddSingleton<NameBiddingViewModel>();
            services.AddSingleton<SettingsViewModel>();
            services.AddSingleton<MainWindowViewModel>();

            services.AddSingleton<IClock, SystemClock>();
            services.AddSingleton<IIntervalMessagingService, IntervalMessagingService>();
            services.AddSingleton<IEquipmentAccessor, EquipmentAccessor>();
            services.AddSingleton<IMemoryAccessor, MemoryAccessor>();
            services.AddSingleton<IGameMomentAccessor, GameMomentAccessor>();
            services.AddSingleton<IMenuColorAccessor, MenuColorAccessor>();
            services.AddSingleton<IGilAccessor, GilAccessor>();
            services.AddSingleton<IInventoryAccessor, InventoryAccessor>();
            services.AddSingleton<IMateriaAccessor, MateriaAccessor>();
            services.AddSingleton<INameAccessor, NameAccessor>();
            services.AddSingleton<IStatusAccessor, StatusAccessor>();
            services.AddSingleton<ITwitchClient, TwitchClient>();
            services.AddSingleton<IDialogService, DialogService>();

            services.RegisterEquipmentData();

            services.RegisterBattleCommand<StatusEffectCommand>();

            services.RegisterNonBattleCommand<WeaponCommand>();
            services.RegisterNonBattleCommand<ArmletCommand>();
            services.RegisterNonBattleCommand<AccessoryCommand>();
            services.RegisterNonBattleCommand<PauperCommand>();

            services.RegisterTwitchCommand<PaletteCommand>();
            services.RegisterTwitchCommand<RainbowCommand>();
            services.RegisterTwitchCommand<MakoCommand>();
            services.RegisterTwitchCommand<ItemCommand>();
            services.RegisterTwitchCommand<MateriaCommand>();
            services.RegisterTwitchCommand<CostsCommand>();
            services.RegisterTwitchCommand<GiveGilCommand>();
            services.RegisterTwitchCommand<NameBidsCommand>();
            services.RegisterTwitchCommand<MenuCommand>();
            services.RegisterTwitchCommand<NameCommand>();
            services.RegisterTwitchCommand<RefreshCommand>();
            services.RegisterTwitchCommand<BalanceCommand>();
            services.RegisterTwitchCommand<HelpCommand>();
            services.RegisterTwitchCommand<I7Command>();

            services.AddSingleton<IChatBot, ChatBot>();

            services.AddSingleton(typeof(IDataStore<>), typeof(FileDataStore<>));

            services.AddSingleton<DataLoader>();

            services.AddSingleton<ISettingsStore, SettingsStore>();
            services.AddSingleton<GilBank>();
            services.AddSingleton<ColorPaletteCollection>();
            services.AddSingleton<MainWindow>();

            services.AddLogging(config =>
            {
                config.AddSerilog();
            });
        }
    }
}
