using Autofac;
using InteractiveSeven.Core.Events;
using InteractiveSeven.Twitch;
using InteractiveSeven.UI.Settings;
using System;
using System.Reflection;
using System.Windows.Forms;
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

            builder.RegisterType<TwitchClient>()
                .AsImplementedInterfaces().AsSelf().SingleInstance();
        }

        private static void InitializeSettings()
        {
            new SettingsStore().EnsureExists();
        }
    }
}
