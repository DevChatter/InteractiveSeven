using System;
using System.Threading.Tasks;

namespace InteractiveSeven.Core
{
    public static class TaskExtensions
    {
        /// <summary>
        /// Awaits the task, so error can be handled (if needed).
        /// </summary>
        /// <param name="task">task to run</param>
        /// <param name="continueOnCapturedContext">value to pass to ConfigureAwait</param>
        /// <param name="onException">exception handling, won't catch exceptions if left null</param>
        public static async void RunInBackgroundSafely(this Task task,
            bool continueOnCapturedContext = true,
            Action<Exception> onException = null)
        {
            try
            {
                await task.ConfigureAwait(continueOnCapturedContext);
            }
            catch (Exception ex) when (onException != null)
            {
                onException(ex);
            }
        }
    }
}