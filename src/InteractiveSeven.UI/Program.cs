using Autofac;
using FluentMigrator.Runner;
using InteractiveSeven.Core.Data;
using InteractiveSeven.Core.Events;
using InteractiveSeven.Core.Settings;
using InteractiveSeven.Sqlite;
using InteractiveSeven.Twitch;
using InteractiveSeven.UI.Migrations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Windows.Forms;

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
            IContainer container = RegisterDependencies();

            RunDatabaseMigration();

            InitializeSettings(container);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<MainView>());
        }

        private static void RunDatabaseMigration()
        {
            var serviceProvider = new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSQLite()
                    .WithGlobalConnectionString(DapperRepository.CONNECTION_STRING)
                    .ScanIn(typeof(Migration0001AddSettingsTable).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);

            using (var scope = serviceProvider.CreateScope())
            {
                var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                runner.MigrateUp();
            }
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
            Assembly sqliteAssembly = Assembly.GetAssembly(typeof(DapperRepository));
            builder.RegisterAssemblyTypes(
                    winFormsAssembly, coreAssembly, twitchAssembly, sqliteAssembly)
                .AsImplementedInterfaces().AsSelf().SingleInstance();
        }

        private static void InitializeSettings(IContainer container)
        {
            var repository = container.Resolve<IRepository>();
            var settings = repository.GetAllSettings();

            ApplicationSettings.Instance.Initialize(settings);
        }
    }
}
