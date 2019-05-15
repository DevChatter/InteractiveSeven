using ReactiveUI;
using System;
using InteractiveSeven.Core.Settings;

namespace InteractiveSeven.UI.ViewModels
{
    public static class ViewModelExtensions
    {
        public static void RaiseAndSetPropertyIfChanged<T>(
            this ReactiveObject vm, T value,
            Func<ApplicationSettings, T> getter,
            Action<ApplicationSettings, T> setter)
        {
            if (value.Equals(getter(ApplicationSettings.Instance))) return;

            vm.RaisePropertyChanging();
            setter(ApplicationSettings.Instance, value);
            vm.RaisePropertyChanged();
        }
    }
}