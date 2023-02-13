using System;
using System.Windows.Controls;
using InteractiveSeven.Core.Settings;
using Serilog;
using Serilog.Extensions.Logging;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace InteractiveSeven.Startup
{
    internal static class ConfigureLogging
    {
        internal static ILogger Initial()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File("logs\\i7log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            var logger = new SerilogLoggerProvider(Log.Logger).CreateLogger(nameof(SettingsStore));
            return logger;
        }

        internal static void WithRichTextOutput(RichTextBox outputBox)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.RichTextBox(outputBox)
                .WriteTo.File("logs\\i7log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }
    }
}
