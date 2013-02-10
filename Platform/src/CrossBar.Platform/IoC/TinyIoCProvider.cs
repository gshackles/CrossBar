using System;
using Cirrious.MvvmCross.Interfaces.IoC;
using TinyIoC;

namespace CrossBar.Platform.IoC
{
    public class TinyIoCProvider : IMvxIoCProvider
    {
        private readonly TinyIoCContainer _container;

        public TinyIoCProvider()
        {
            _container = new TinyIoCContainer();
        }

        public bool SupportsService<T>() where T : class
        {
            return _container.CanResolve<T>();
        }

        public T GetService<T>() where T : class
        {
            return _container.Resolve<T>();
        }

        public bool TryGetService<T>(out T service) where T : class
        {
            return _container.TryResolve(out service);
        }

        public void RegisterServiceType<TFrom, TTo>() where TFrom : class where TTo : class, TFrom
        {
            _container.Register<TFrom, TTo>();
        }

        public void RegisterServiceInstance<TInterface>(TInterface theObject) where TInterface : class
        {
            _container.Register<TInterface>(theObject);
        }

        public void RegisterServiceInstance<TInterface>(Func<TInterface> theConstructor) where TInterface : class
        {
            _container.Register<TInterface>((c, p) => theConstructor());
        }
    }
}