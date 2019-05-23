using Autofac;
using InteractiveSeven.Core.Events;
using InteractiveSeven.Twitch;
using System;
using System.Reflection;
using System.Windows.Forms;
using InteractiveSeven.Core.Settings;
using InteractiveSeven.UI.UserControls;
using InteractiveSeven.UI.ViewModels;
using TwitchLib.Client;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.UI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            InitializeSettings();

            IContainer container = RegisterDependencies();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<MainView>());
        }

        private static IContainer RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            RegisterDependencies(builder);

            IContainer container = builder.Build();
            return container;
        }

        private static void RegisterDependencies(ContainerBuilder builder)
        {
            Assembly winFormsAssembly = Assembly.GetExecutingAssembly();
            Assembly coreAssembly = Assembly.GetAssembly(typeof(BaseDomainEvent));
            Assembly twitchAssembly = Assembly.GetAssembly(typeof(ChatBot));
            builder.RegisterAssemblyTypes(winFormsAssembly, coreAssembly, twitchAssembly)
                .AsImplementedInterfaces().AsSelf().SingleInstance();

            builder.RegisterType<NameBidding>().AsSelf().InstancePerDependency();
            builder.RegisterType<NameBidsViewModel>().AsSelf().InstancePerDependency();

            builder.RegisterType<TwitchClient>()
                .AsImplementedInterfaces().AsSelf().SingleInstance();
        }

        private static void InitializeSettings()
        {
            new SettingsStore().EnsureExists();
        }
    }
}
