using System.Collections.Generic;
using InteractiveSeven.Commands;
using InteractiveSeven.Core;
using InteractiveSeven.Core.Chat;
using InteractiveSeven.Core.Commands;
using InteractiveSeven.Core.Data;
using InteractiveSeven.Core.Diagnostics;
using InteractiveSeven.Core.Diagnostics.Memory;
using InteractiveSeven.Core.Emitters;
using InteractiveSeven.Core.FinalFantasy;
using InteractiveSeven.Core.IntervalMessages;
using InteractiveSeven.Core.Models;
using InteractiveSeven.Core.MvvmCommands;
using InteractiveSeven.Core.Payments;
using InteractiveSeven.Core.Services;
using InteractiveSeven.Core.Settings;
using InteractiveSeven.Core.ViewModels;
using InteractiveSeven.Core.Workloads;
using InteractiveSeven.Services;
using InteractiveSeven.Twitch;
using InteractiveSeven.Twitch.Chat;
using InteractiveSeven.Twitch.Commands;
using InteractiveSeven.Twitch.IntervalMessages;
using InteractiveSeven.Web.Hubs;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Tseng;
using Tseng.lib;
using Tseng.RunOnce;
using TwitchLib.Api;
using TwitchLib.Api.Interfaces;
using TwitchLib.Client;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Startup
{
    public static class DependencyRegistrar
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient(typeof(IList<>), typeof(List<>));

            services.AddSingleton<WorkloadCoordinator>();

            services.AddSingleton<MenuColorViewModel>();
            services.AddSingleton<NameBiddingViewModel>();
            services.AddSingleton<StreamOverlayViewModel>();
            services.AddSingleton<SettingsViewModel>();
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<TwitchAuthViewModel>();
            services.AddSingleton<ThemeViewModel>();

            services.AddSingleton<IModded, Modded>();

            services.AddSingleton<PartyStatusViewModel>();

            services.AddTransient<IStatusHubEmitter, StatusHubEmitter>();

            services.AddSingleton<IClock, SystemClock>();
            services.AddSingleton<IIntervalMessagingService, IntervalMessagingService>();
            services.AddSingleton<IEquipmentAccessor, EquipmentAccessor>();
            services.AddSingleton<IMemoryAccessor, MemoryAccessor>();
            services.AddSingleton<NativeMemoryReader>();
            services.AddSingleton<IGameMomentAccessor, GameMomentAccessor>();
            services.AddSingleton<IMenuColorAccessor, MenuColorAccessor>();
            services.AddSingleton<IGilAccessor, GilAccessor>();
            services.AddSingleton<IGpAccessor, GpAccessor>();
            services.AddSingleton<IInventoryAccessor, InventoryAccessor>();
            services.AddSingleton<IMateriaAccessor, MateriaAccessor>();
            services.AddSingleton<IBattleInfoAccessor, BattleInfoAccessor>();
            services.AddSingleton<INameAccessor, NameAccessor>();
            services.AddSingleton<IStatusAccessor, StatusAccessor>();
            services.AddSingleton<ITwitchAPI, TwitchAPI>();
            services.AddSingleton<IChatClient, TwitchChatClient>();
            services.AddSingleton<ITwitchClient, TwitchClient>();
            services.AddSingleton<IDialogService, DialogService>();

            services.AddTransient<IThemeChanger, ThemeChanger>();

            services.RegisterEquipmentData();

            services.RegisterTwitchCommand<LockCommand>();
            services.RegisterTwitchCommand<UnlockCommand>();

            services.RegisterBattleCommand<StatusEffectCommand>();
            services.RegisterBattleCommand<EsunaCommand>();
            services.RegisterBattleCommand<HealStatusEffectCommand>();

            services.RegisterNonBattleCommand<WeaponCommand>();
            services.RegisterNonBattleCommand<ArmletCommand>();
            services.RegisterNonBattleCommand<AccessoryCommand>();
            services.RegisterNonBattleCommand<PauperCommand>();
            services.RegisterNonBattleCommand<RemovePlayerGilCommand>();
            services.RegisterNonBattleCommand<GivePlayerGilCommand>();
            services.RegisterNonBattleCommand<RemovePlayerGpCommand>();
            services.RegisterNonBattleCommand<GivePlayerGpCommand>();

            services.RegisterTwitchCommand<PaletteCommand>();
            services.RegisterTwitchCommand<RainbowCommand>();
            services.RegisterTwitchCommand<MakoCommand>();
            services.RegisterTwitchCommand<DropCommand>();
            services.RegisterTwitchCommand<ItemCommand>();
            services.RegisterTwitchCommand<MateriaCommand>();
            services.RegisterTwitchCommand<CostsCommand>();
            services.RegisterTwitchCommand<RemoveGilCommand>();
            services.RegisterTwitchCommand<GiveGilCommand>();
            services.RegisterTwitchCommand<NameBidsCommand>();
            services.RegisterTwitchCommand<MenuCommand>();
            services.RegisterTwitchCommand<NameCommand>();
            services.RegisterTwitchCommand<RefreshCommand>();
            services.RegisterTwitchCommand<BalanceCommand>();
            services.RegisterTwitchCommand<HelpCommand>();
            services.RegisterTwitchCommand<I7Command>();

            services.AddSingleton<IChatBot, ChatBot>();

            services.AddSingleton<IGameDatabaseLoader, GameDatabaseLoader>();
            services.AddSingleton<GameDatabase>();
            services.AddSingleton<FF7BattleMap>();
            services.AddSingleton<ProcessConnector>();
            services.AddSingleton<TsengMonitor>();

            services.AddSingleton(typeof(IDataStore<>), typeof(FileDataStore<>));

            services.AddSingleton<DataLoader>();

            services.AddSingleton<ISettingsStore, SettingsStore>();
            services.AddSingleton<GilBank>();
            services.AddSingleton<PaymentProcessor>();
            services.AddSingleton<ColorPaletteCollection>();
            services.AddSingleton<IShowTwitchAuthCommand, ShowTwitchAuthCommand>();
            services.AddSingleton<MainWindow>();

            services.AddLogging(config =>
            {
                config.AddSerilog();
            });

            services.AddMvcCore();
        }
    }
}
