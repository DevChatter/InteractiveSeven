using InteractiveSeven.Core.Settings;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace InteractiveSeven
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            InitializeSettings();
        }

        private static void InitializeSettings()
        {
            new SettingsStore().EnsureExists();
        }
    }
}
