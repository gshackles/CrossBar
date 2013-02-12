using System;
using System.Threading;
using CrossBar.Platform.ViewModels;

namespace CrossBar.Platform.Tests.Extensions
{
    public static class ViewModelExtensions
    {
        public static void AllowToComplete(this bool loadingIndicator)
        {
            const int maxWaitTime = 1000;
            const int increment = 10;
            int timer = 0;

            while (timer < maxWaitTime && loadingIndicator)
            {
                timer += increment;

#if NETFX_CORE
                new System.Threading.ManualResetEvent(false).WaitOne(increment);
#else
                Thread.Sleep(increment);
#endif
            }
        }

        public static void WaitUntilLoaded(this ViewModelBase viewModel)
        {
            viewModel.IsLoading.AllowToComplete();
        }
    }
}