using System.Collections.Generic;
using DevChatter.InteractiveGames.Core.Seven.Commands;
using DevChatter.InteractiveGames.Core.Seven.Extensions;
using DevChatter.InteractiveGames.Core.Seven.Services;
using DevChatter.InteractiveGames.Core.Seven.Tseng.Memory;
using DevChatter.InteractiveGames.SharedUI;
using InteractiveSeven.Core;
using InteractiveSeven.Core.Bidding.Moods;
using InteractiveSeven.Core.Data;
using InteractiveSeven.Core.Diagnostics.Memory;
using InteractiveSeven.Core.Emitters;
using InteractiveSeven.Core.IntervalMessages;
using InteractiveSeven.Core.Moods;
using InteractiveSeven.Core.MvvmCommands;
using InteractiveSeven.Core.Payments;
using InteractiveSeven.Core.Services;
using InteractiveSeven.Core.Settings;
using InteractiveSeven.Twitch;
using InteractiveSeven.Twitch.Commands;
using InteractiveSeven.Twitch.IntervalMessages;
using InteractiveSeven.Twitch.Payments;
using InteractiveSeven.Web.Hubs;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Tseng;
using TwitchLib.Api;
using TwitchLib.Api.Interfaces;
using TwitchLib.Client;
using TwitchLib.Client.Interfaces;

namespace DevChatter.InteractiveGames.Core.Seven
{
    public static class DependencyRegistrar
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient(typeof(IList<>), typeof(List<>));


            RegisterMoods(services);


            services.AddTransient<IStatusHubEmitter, StatusHubEmitter>();

            services.AddSingleton<IClock, SystemClock>();
            services.AddSingleton<IIntervalMessagingService, IntervalMessagingService>();
            services.AddSingleton<AoBScanner>();
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
            services.AddSingleton<ITwitchClient, TwitchClient>();
            services.AddSingleton<IDialogService, DialogService>();

            services.AddTransient<IThemeChanger, ThemeChanger>();

            services.RegisterEquipmentData();

            services.RegisterTwitchCommand<LockCommand>();
            services.RegisterTwitchCommand<UnlockCommand>();

            services.RegisterTwitchCommand<CostsCommand>();
            services.RegisterTwitchCommand<RemoveGilCommand>();
            services.RegisterTwitchCommand<GiveGilCommand>();
            services.RegisterTwitchCommand<RefreshCommand>();
            services.RegisterTwitchCommand<BalanceCommand>();
            services.RegisterTwitchCommand<HelpCommand>();
            services.RegisterTwitchCommand<I7Command>();

            services.RegisterTwitchCommand<MoodBidsCommand>();
            services.RegisterTwitchCommand<ChangeMoodCommand>();

            services.AddSingleton<IChatBot, ChatBot>();


            services.AddSingleton(typeof(IDataStore<>), typeof(FileDataStore<>));

            services.AddSingleton<DataLoader>();

            services.AddSingleton<ISettingsStore, SettingsStore>();
            services.AddSingleton<GilBank>();
            services.AddSingleton<MemoryFreezer>();
            services.AddSingleton<PaymentProcessor>();
            services.AddSingleton<IShowTwitchAuthCommand, ShowTwitchAuthCommand>();
            services.AddSingleton<MainWindow>();

            services.AddSharedWindows();

            services.AddLogging(config =>
            {
                config.AddSerilog();
            });

            services.AddMvcCore();
        }

        private static void RegisterMoods(IServiceCollection services)
        {
            services.AddSingleton<MoodBidding>();
            services.AddSingleton<Mood, DangerMood>();
            services.AddSingleton<Mood, NormalMood>();
            services.AddSingleton<Mood, PeacefulMood>();
            services.AddSingleton<Mood, NothingMood>();
            services.AddSingleton<Mood, LooseChangeMood>();
        }
    }
}
