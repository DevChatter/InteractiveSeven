﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace InteractiveSeven.Core.Settings
{
    public abstract class ObservableSettingsBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}