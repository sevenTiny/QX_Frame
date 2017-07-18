using Autofac;
using System;

namespace QX_Frame.App.Web
{
    public class ChannelFactory<TService> : IDisposable
    {
        private IContainer _container;
        public ChannelFactory(IContainer container)
        {
            _container = container;
        }
        public TService CreateChannel() => _container.Resolve<TService>();

        public void Dispose() { }
    }
}
