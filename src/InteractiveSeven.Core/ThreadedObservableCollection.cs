using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Threading;

namespace InteractiveSeven.Core
{
    public class ThreadedObservableCollection<T> : ObservableCollection<T>
    {
        private SynchronizationContext synchronizationContext = SynchronizationContext.Current;

        public ThreadedObservableCollection()
        {
        }

        public ThreadedObservableCollection(IEnumerable<T> list)
           : base(list)
       {
       }

    protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
        if (SynchronizationContext.Current == synchronizationContext)
        {
            RaiseCollectionChanged(e);
        }
        else
        {
            synchronizationContext.Send(RaiseCollectionChanged, e);
        }
    }

    private void RaiseCollectionChanged(object param)
    {
        base.OnCollectionChanged((NotifyCollectionChangedEventArgs)param);
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        if (SynchronizationContext.Current == synchronizationContext)
        {
            RaisePropertyChanged(e);
        }
        else
        {
            synchronizationContext.Send(RaisePropertyChanged, e);
        }
    }

    private void RaisePropertyChanged(object param)
    {
        base.OnPropertyChanged((PropertyChangedEventArgs)param);
    }
}
}
