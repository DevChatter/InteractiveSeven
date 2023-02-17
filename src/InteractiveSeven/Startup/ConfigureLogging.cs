using System.Windows.Controls;
using Serilog;

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
            return Log.Logger;
        }

        internal static void WithRichTextOutput(RichTextBox outputBox)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.RichTextBox(outputBox)
                .WriteTo.File("logs\\i7log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            Log.Logger.Information("I7 Logger Display Set Up");
        }
    }
}
