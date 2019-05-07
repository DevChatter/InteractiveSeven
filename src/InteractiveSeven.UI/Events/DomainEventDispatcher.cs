using Autofac;
using InteractiveSeven.Core.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace InteractiveSeven.UI.Events
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IComponentContext _container;

        public DomainEventDispatcher(IComponentContext container)
        {
            _container = container;
        }

        public void Dispatch(BaseDomainEvent domainEvent)
        {
            Type handlerType = typeof(IHandle<>).MakeGenericType(domainEvent.GetType());
            Type wrapperType = typeof(DomainEventHandler<>).MakeGenericType(domainEvent.GetType());
            IEnumerable handlers = (IEnumerable)_container.Resolve(typeof(IEnumerable<>).MakeGenericType(handlerType));
            IEnumerable<DomainEventHandler> wrappedHandlers = handlers.Cast<object>()
                .Select(handler => (DomainEventHandler)Activator.CreateInstance(wrapperType, handler));

            foreach (DomainEventHandler handler in wrappedHandlers)
            {
                handler.Handle(domainEvent);
            }
        }

        private abstract class DomainEventHandler
        {
            public abstract void Handle(BaseDomainEvent domainEvent);
        }

        private class DomainEventHandler<T> : DomainEventHandler
            where T : BaseDomainEvent
        {
            private readonly IHandle<T> _handler;

            public DomainEventHandler(IHandle<T> handler)
            {
                _handler = handler;
            }

            public override void Handle(BaseDomainEvent domainEvent)
            {
                _handler.Handle((T)domainEvent);
            }
        }
    }
}