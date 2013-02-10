using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Input;

namespace CrossBar.Platform.Tests.Extensions
{
    public static class IMvxCommandExtensions
    {
        public static void ExecuteTest(this ICommand command, object parameter = null)
        {
            command.Execute(parameter);

            // TODO: hack! is there a better way to do this?
#if NETFX_CORE
            new System.Threading.ManualResetEvent(false).WaitOne(50);
#else
            Thread.Sleep(50);
#endif
        }
    }
}
