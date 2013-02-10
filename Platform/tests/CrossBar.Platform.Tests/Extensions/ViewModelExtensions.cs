using System;
using System.Threading;
using CrossBar.Platform.ViewModels;
using CrossBar.Platform.ViewModels.Parameters;

namespace CrossBar.Platform.Tests.Extensions
{
    public static class ViewModelExtensions
    {
        public static void WaitUntilLoaded<TParameter>(this ViewModelBase<TParameter> viewModel) where TParameter : ParametersBase
        {
            const int maxWaitTime = 100;
            const int increment = 10;
            int timer = 0;

            while (timer < maxWaitTime && viewModel.IsLoading)
            {
                timer += increment;

#if NETFX_CORE
                new System.Threading.ManualResetEvent(false).WaitOne(increment);
#else
                Thread.Sleep(increment);
#endif
            }
        }
    }
}