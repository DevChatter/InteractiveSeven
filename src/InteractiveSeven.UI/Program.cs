using System;
using System.Reflection;
using System.Windows.Forms;
using Autofac;
using InteractiveSeven.Core.Events;
using InteractiveSeven.Twitch;
using InteractiveSeven.UI.Settings;

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
            var builder = new ContainerBuilder();

            Assembly winFormsAssembly = Assembly.GetExecutingAssembly();
            Assembly coreAssembly = Assembly.GetAssembly(typeof(BaseDomainEvent));
            Assembly twitchAssembly = Assembly.GetAssembly(typeof(ChatBot));
            builder.RegisterAssemblyTypes(winFormsAssembly, coreAssembly, twitchAssembly)
                .AsImplementedInterfaces().AsSelf().SingleInstance();

            var element = InteractionSettings.Settings.Interactions.ByName("MenuColor");

            InteractionSettings.Settings.Interactions.Add(new InteractionElement("Test1", true));

            IContainer container = builder.Build();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<MainView>());
        }
    }
}
