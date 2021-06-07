using System.Collections.Generic;
using DevChatter.InteractiveGames.Core.Seven.Extensions;
using InteractiveSeven.Core.Data;
using InteractiveSeven.Core.Diagnostics;
using InteractiveSeven.Core.Diagnostics.Memory;
using InteractiveSeven.Core.FinalFantasy;
using InteractiveSeven.Core.Models;
using InteractiveSeven.Core.Moods;
using InteractiveSeven.Core.ViewModels;
using InteractiveSeven.Core.Workloads;
using InteractiveSeven.Twitch.Commands;
using Microsoft.Extensions.DependencyInjection;
using Tseng;
using Tseng.RunOnce;

namespace InteractiveSeven.Startup
{
    public static class FF7Registrar
    {
        public static void AddFF7Registry(this IServiceCollection services)
        {
            services.AddTransient(typeof(IList<>), typeof(List<>));

            services.AddSingleton<WorkloadCoordinator>();
            services.AddSingleton<MoodEnforcer>();

            services.RegisterViewModels();

            services.AddSingleton<IEquipmentAccessor, EquipmentAccessor>();
            services.AddSingleton<IMemoryAccessor, MemoryAccessor>();

            services.AddSingleton<IGameDatabaseLoader, GameDatabaseLoader>();
            services.AddSingleton<GameDatabase>();
            services.AddSingleton<FF7BattleMap>();
            services.AddSingleton<ProcessConnector>();
            services.AddSingleton<TsengMonitor>();
            services.AddSingleton<ColorPaletteCollection>();

        }

        private static void RegisterCommands(this IServiceCollection services)
        {
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

            services.RegisterTwitchCommand<NameBidsCommand>();
            services.RegisterTwitchCommand<MenuCommand>();
            services.RegisterTwitchCommand<NameCommand>();

        }

        private static void RegisterViewModels(this IServiceCollection services)
        {
            services.AddSingleton<MenuColorViewModel>();
            services.AddSingleton<NameBiddingViewModel>();
            services.AddSingleton<StreamOverlayViewModel>();
            services.AddSingleton<SettingsViewModel>();
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<TwitchAuthViewModel>();
            services.AddSingleton<ThemeViewModel>();

            services.AddSingleton<PartyStatusViewModel>();

        }
    }
}
