using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InteractiveSeven.Core.Events
{

    public static class DomainEvents
    {
        //[ThreadStatic] //so that each thread has its own callbacks
        private static List<Delegate> _callbacks;

        public static void Register<T>(Action<T> callback)
            where T : BaseDomainEvent
        {
            if (_callbacks == null)
                _callbacks = new List<Delegate>();

            _callbacks.Add(callback);
        }

        public static void Raise<T>(T args)
            where T : BaseDomainEvent
        {
            if (_callbacks is null) return;

            foreach (var callBack in _callbacks)
            {
                if (callBack is Action<T> action)
                {
                    action(args);
                }
            }
        }

        public static void Clear()
        {
            _callbacks?.Clear();
        }
    }
}
