using Autofac;
using InteractiveSeven.Core.Events;
using InteractiveSeven.Core.Settings;
using InteractiveSeven.Twitch;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Newtonsoft.Json;

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

            ScanAssemblies(builder);

            IContainer container = builder.Build();
            return container;
        }

        private static void ScanAssemblies(ContainerBuilder builder)
        {
            Assembly winFormsAssembly = Assembly.GetExecutingAssembly();
            Assembly coreAssembly = Assembly.GetAssembly(typeof(BaseDomainEvent));
            Assembly twitchAssembly = Assembly.GetAssembly(typeof(ChatBot));
            builder.RegisterAssemblyTypes(winFormsAssembly, coreAssembly, twitchAssembly)
                .AsImplementedInterfaces().AsSelf().SingleInstance();
        }

        private static void InitializeSettings()
        {
            const string settingsFileName = "appsettings.json";
            if (File.Exists(settingsFileName))
            {
                string json = File.ReadAllText(settingsFileName);
                ApplicationSettings.LoadFromJson(json);
            }
            else
            {
                string text = JsonConvert.SerializeObject(ApplicationSettings.Instance);
                File.WriteAllText(settingsFileName, text);
            }
        }
    }
}
